using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;
using BookStore.Repositories.Implementations;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using BookStore.Services.Implementations;
using BookStore.Services.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BookStore.Persistence.Seeders;
using BookStore.Entities;
using MusicStore.Services.Interfaces;
using MusicStore.Services.Implementations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using BookStore.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using BookStore.Dto.Response;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(AppContext.BaseDirectory, "logs", "log.txt");
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(logPath,
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

try
{
    builder.Logging.AddSerilog(logger);
    logger.Information($"LOG IN {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "NO ENV"}");

    builder.Services.Configure<AppSettings>(builder.Configuration);

    var corsConfiguration = "BookStoreCors";
    builder.Services.AddCors(setup =>
    {
        setup.AddPolicy(corsConfiguration, policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader().WithExposedHeaders(new string[] { "x-total" });
            policy.AllowAnyMethod();
        });
    });

    // Add services to the container.

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(FilterExceptions));
    });

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new BaseResponse
            {
                Success = false,
                ErrorMessage = string.Join("; ", errors) // Une los mensajes de error en un solo string.
            };

            return new BadRequestObjectResult(response);
        };
    });


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<BookStoreDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
    });
    #region Configuración de politicas y JWT
    builder.Services.AddIdentity<BookStoreUserIdentity, IdentityRole>(policies =>
    {
        policies.Password.RequireDigit = true;
        policies.Password.RequiredLength = 8;
        policies.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<BookStoreDbContext>().AddDefaultTokenProviders();

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"] ?? throw new InvalidCastException("JWT key not configurader."));
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
    builder.Services.AddAuthorization();

    builder.Services.AddHttpContextAccessor();
    #endregion

    #region Registro de resvicios
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IOrderBookRepository, OrderBookRepository>();
    builder.Services.AddScoped<IBookService, BookService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IOrderBookService, OrderBookService>();
    builder.Services.AddScoped<UserDataSeeder>();
    builder.Services.AddScoped<BookDataSeeder>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IFileStorage, FileStorageLocal>();
    #endregion

    #region HealthChecks
    builder.Services.AddHealthChecks().AddCheck("selfcheck", () => HealthCheckResult.Healthy()).AddDbContextCheck<BookStoreDbContext>();
    #endregion

    #region Auto Mapper
    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile<BookProfile>();
        config.AddProfile<CustomerProfile>();
        config.AddProfile<OrderProfile>();
        config.AddProfile<OrderBookProfile>();
    });
    #endregion

    if (builder.Environment.IsProduction())
    {
        builder.WebHost.UseUrls("https://192.168.1.98:8081");
    }

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseAuthentication();

    app.UseAuthorization();
    app.UseCors(corsConfiguration);

    app.MapControllers();

    #region Se aplican migraciones y datos de inicio
    await ApplyMigrationsAndSeedDataAsync(app);
    #endregion

    app.MapHealthChecks("/healthcheck", new()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex, "An unhandled exception occurred during the API initialization.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

static async Task ApplyMigrationsAndSeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any()) await dbContext.Database.MigrateAsync(); 
    var userDataSeeder = scope.ServiceProvider.GetRequiredService<UserDataSeeder>();
    await userDataSeeder.SeedAsync();
    var BookDataSeeder = scope.ServiceProvider.GetRequiredService<BookDataSeeder>();
    await BookDataSeeder.SeedAsync();
}

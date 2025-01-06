using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Persistence;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<BookStoreUserIdentity> userManager;
        private readonly ILogger<UserService> logger;
        private readonly SignInManager<BookStoreUserIdentity> singInManager;
        private readonly IOptions<AppSettings> options;

        public UserService(UserManager<BookStoreUserIdentity> userManager, ILogger<UserService> logger, SignInManager<BookStoreUserIdentity> singInManager, IOptions<AppSettings> options)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.singInManager = singInManager;
            this.options = options;
        }

        public async Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var response = new BaseResponseGeneric<LoginResponseDto>();
            try
            {
                var resultado = await singInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
                if (resultado.Succeeded) 
                {
                    var user = await userManager.FindByEmailAsync(request.UserName);
                    response.Success = true;
                    response.Data = await ContruirToken(user);
                    logger.LogInformation("User {0} logged in successfully", request.UserName);
                }
                else
                {
                    response.ErrorMessage = "Usuario y/o contraseña incorrecta.";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al consultar la información.";
                logger.LogError(ex, "{} {}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        private async Task<LoginResponseDto> ContruirToken(BookStoreUserIdentity user)
        {
            var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.Email,user.Email),
               new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
           };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //firmando el JWT
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Jwt.JWTKey)); 
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddSeconds(options.Value.Jwt.LifetimeInSeconds);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, signingCredentials: credenciales, expires: expiracion);
            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiracion
            };
        }
    }
}

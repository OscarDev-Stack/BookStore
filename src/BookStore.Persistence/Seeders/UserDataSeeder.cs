using BookStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Persistence.Seeders
{
    public class UserDataSeeder
    {
        private readonly IServiceProvider service;

        public UserDataSeeder(IServiceProvider service)
        {
            this.service = service;
        }
        public async Task SeedAsync()
        {
            var userManager = service.GetRequiredService<UserManager<BookStoreUserIdentity>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var adminRole = new IdentityRole(Constants.RoleAdmin);
            var LibrarianRole = new IdentityRole(Constants.RoleLibrarian);

            if (!await roleManager.RoleExistsAsync(Constants.RoleAdmin))
                await roleManager.CreateAsync(adminRole);
            if (!await roleManager.RoleExistsAsync(Constants.RoleLibrarian))
                await roleManager.CreateAsync(LibrarianRole);

            var adminUser = new BookStoreUserIdentity()
            {
                FirstName = "System",
                LastName = "Administrator",
                UserName = "admin@bookstore.com",
                Email = "admin@bookstore.com",
                Position = "Admin",
                DocumentType = DocumentTypeEnum.employeenumber,
                EmployeeNumber = "A000001",
                EmailConfirmed = true
            };
            if (await userManager.FindByEmailAsync("admin@bookstore.com") is null)
            {
                var result = await userManager.CreateAsync(adminUser, "BookStore1#");
                if (result.Succeeded)
                {
                    adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                    if (adminUser is not null)
                    {
                        await userManager.AddToRoleAsync(adminUser, Constants.RoleAdmin);
                    }
                }
            }

            var LibrarianUser = new BookStoreUserIdentity()
            {
                FirstName = "System",
                LastName = "Librarian",
                UserName = "librarian@bookstore.com",
                Email = "librarian@bookstore.com",
                Position = "Librarian",
                DocumentType = DocumentTypeEnum.employeenumber,
                EmployeeNumber = "A000002",
                EmailConfirmed = true
            };
            if (await userManager.FindByEmailAsync("librarian@bookstore.com") is null)
            {
                var result = await userManager.CreateAsync(LibrarianUser, "BookStore1#");
                if (result.Succeeded)
                {
                    adminUser = await userManager.FindByEmailAsync(LibrarianUser.Email);
                    if (adminUser is not null)
                    {
                        await userManager.AddToRoleAsync(LibrarianUser, Constants.RoleLibrarian);
                    }
                }
            }
        }
    }
}

using FashionAllTheWay.Data;
using FashionAllTheWay.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionAllTheWay.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            await RoleSeeder(services);
            await SeedAdministrator(services);
            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);
            return app;
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category{CategoryName= "Clothes for women"},
                new Category{CategoryName= "Clothes for men"},
                new Category{CategoryName= "Bags"},
                new Category{CategoryName= "Accessories"},
                new Category{CategoryName= "Shoes for women"},
                new Category{CategoryName= "Shoes foe men"},
                
            });
            dataCategory.SaveChanges();
        }
        private static void SeedBrands(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }
            dataBrand.Brands.AddRange(new[]
            {
                new Brand{BrandName= "Guess"},
                new Brand{BrandName= "Versace Jeans Couture"},
                new  Brand{BrandName= "Love Moschino"},
                new  Brand{BrandName= "Desiglau"},
                new Brand{BrandName= "Calvin Klein"},
                new  Brand{BrandName= "Karl Lagerfeld"},
                new  Brand{BrandName= "Michael Kors"},
                new  Brand{BrandName= "Pinko"},
                new  Brand{BrandName= "Tommy Hilfiger"},
                new  Brand{BrandName= "Liu Jo"},
            });
            dataBrand.SaveChanges();
        }
        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.PhoneNumber = "0888888888";
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                var result = await userManager.CreateAsync
                (user, "Admin123456");
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Administrator").Wait();

            }
        }
    }
}

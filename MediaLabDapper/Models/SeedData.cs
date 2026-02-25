using MediaLabDapper.Models;
using Microsoft.AspNetCore.Identity;

namespace MediaLabDapper.Models
{
    public static class SeedData
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Rolleri oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Patient"))
                await roleManager.CreateAsync(new IdentityRole("Patient"));

            // Admin kullanıcısı oluştur
            var adminUser = await userManager.FindByEmailAsync("admin@medilab.com");
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    FullName = "Admin",
                    UserName = "admin@medilab.com",
                    Email = "admin@medilab.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
using System;
using CMSDb.DbModels;
using CMSDb.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace CMSAPI;

public class DbInitializer
{
    public static async System.Threading.Tasks.Task SeedData(CmsIdentityContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                new() { DisplayName = "Bob", UserName = "bob@test.com", Email = "bob@test.com" },
                new() { DisplayName = "Tom", UserName = "tom@test.com", Email = "tom@test.com" },
                new() { DisplayName = "Jane", UserName = "jane@test.com", Email = "jane@test.com" }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }

        await context.SaveChangesAsync();
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mahmoud Amin",
                    Email = "mahmoud886.amin@gmail.com",
                    UserName = "mahmoud886.amin",
                    Address = new Address()
                    {
                        FirstName = "Mahmoud",
                        LastName = "Amin",
                        Country = "Egypt",
                        City = "Giza",
                        Street = "6th Of October"

                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}

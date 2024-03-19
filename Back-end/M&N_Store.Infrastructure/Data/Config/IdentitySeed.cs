using M_N_Store.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_N_Store.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                //create new user
                var user = new AppUser
                {
                    DisplayName = "Nabih",
                    Email = "Mohammed.nabih68@gmail.com",
                    UserName = "Muhammad_Nabih",
                    Address = new Address
                    {
                        FirstName = "Muhammad",
                        LastName = "Nabih",
                        City = "Cairo",
                        State = "15 Mayo City",
                        Street = "Street 3",
                        ZipCode = "682002"

                    }
                };
                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        }
    }
}

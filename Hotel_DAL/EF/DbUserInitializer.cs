using Hotel_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAL.EF
{
    public class DbUserInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            string AdminNumber = "0684387784";
            string AdminPassword = "111111";

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            }
            if (await roleManager.FindByNameAsync("Guest") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Guest"));
            }
            if (await userManager.FindByNameAsync(AdminNumber) == null)
            {
                User admin = new User()
                {
                    Name = "Anton",
                    Surname = "Antonov",
                    Address = "MySrteet",
                    PhoneNumber = "0684387784",
                    UserName = "0684387784",
                };
                IdentityResult result = await userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    await userManager.AddToRoleAsync(admin, "Guest");
                }

                User user = new User()
                {
                    Name = "Misha",
                    Surname = "Antonov",
                    Address = "Other Street",
                    PhoneNumber = "1111",
                    UserName = "1111",
                };
                IdentityResult result2 = await userManager.CreateAsync(user, "222222");
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Guest");
                }


            }
        }
    }
}

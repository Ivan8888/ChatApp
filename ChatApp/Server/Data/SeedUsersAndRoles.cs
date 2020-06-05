using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;
using System.Security.Claims;

namespace FiscalClientMVC.Data
{
    public class SeedUsersAndRoles
    {
        public static void CreateInitialUsers(UserManager<ChatUser> _userManager, RoleManager<IdentityRole> _roleManager, SignInManager<ChatUser> _signInManager)
        {

            ChatUser user1 = new ChatUser()
            {
                FirstName = "Ivan",
                LastName = "Milutinovic",
                UserName = "user1",
            };
            _userManager.CreateAsync(user1, "user1").Wait();

            ChatUser user2 = new ChatUser()
            {
                FirstName = "Mirko",
                LastName = "Rajovic",
                UserName = "user2",
            };
            _userManager.CreateAsync(user2, "user1").Wait();
        }
    }
}

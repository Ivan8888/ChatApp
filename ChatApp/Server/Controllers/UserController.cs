using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<ChatUser> _userManager;

        public UserController(UserManager<ChatUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<UserBasicInfo> GetAll()
        {
            var users = from u in _userManager.Users
                               select new UserBasicInfo
                               {
                                  UserName = u.UserName,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName
                               };
            return users;
        }
    }
}
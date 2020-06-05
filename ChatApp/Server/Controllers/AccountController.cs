using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        UserManager<ChatUser> _userManager;
        SignInManager<ChatUser> _signInManager;

        public AccountController(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ChatUser chat_user = new ChatUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                IdentityResult result = _userManager.CreateAsync(chat_user, model.Password).Result;
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return StatusCode(400);
            }
        }

        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return StatusCode(400);
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using WebApplication3.DBContext;
using WebApplication3.Model;
using WebApplication3.Services;
using static WebApplication3.Model.General;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication3.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private UserServices UserServices;
        public UsersController()
        {
            UserServices = new UserServices();
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]LoginModel VM)
        {
            string rs = UserServices.Authenticate(VM);
            ResponMessages result = new ResponMessages();
            result.messages = rs;
            return new JsonResult(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel VM)
        {
            ResponMessages result = UserServices.Login(VM);
            return new JsonResult(result);
        }


        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] UserRefreshToken VM)
        {
            ResponMessages result = new ResponMessages();
            if (VM != null)
            {
                result = UserServices.RefreshToken(VM);
                result.Status = 200;
            }
            return new JsonResult(result);
        }
    }
}

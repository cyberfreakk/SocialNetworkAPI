using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Logger;
using UserService.Model;
using UserService.Services;

namespace AuthApi.Controllers
{
    [TypeFilter(typeof(LoggingAspect))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService service;
        public AuthController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public  IActionResult Register([FromBody] User user)
        {
            try
            {
                return Ok(service.Register(this.User, user, "User"));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }
        [HttpPost("register-admin")]
        public IActionResult RegisterAdmin([FromBody] User user)
        {
            try
            {
                return Ok(service.Register(this.User, user, "Admin"));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel user)
        {
            try
            {
                return Ok(service.login(this.User, user ));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("profile/{UserName}")]
        public IActionResult getProfileByUserName(string UserName)
        {
            try
            {
                return Ok(service.getUser(this.User,UserName));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
        [HttpGet("profile")]
        public IActionResult getProfile()
        {

            try
            {
                return Ok(service.getUser(this.User));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("logout")]
        public IActionResult logout()
        {
            try
            {
                return Ok(service.signout());
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPut("changePassword")]
        public IActionResult changePassword(ChangePasswordModel model)
        {
            try
            {
                return Ok(service.changePassword(this.User, model));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}

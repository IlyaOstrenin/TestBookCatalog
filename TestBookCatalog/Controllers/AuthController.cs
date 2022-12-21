using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// registration or user login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<string> Login(Login model) => await _userService.SaveAsync(model);

        /// <summary>
        /// user authentication
        /// </summary>
        /// <remarks>
        /// Admin user:
        ///
        ///     {
        ///        "phoneNumber": "79000000000",
        ///        "code": "1234"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("validate")]
        public async Task<ValidateResponse> Validate(Validate model)
        {
            var user = await _userService.GetAsync(model.PhoneNumber);

            if (!model.Code.Equals(FastFields.SMSCode))
                throw new InvalidOperationException("Invalid code");

            return JwtOptions.BuildValidateResponse(user);
        }

        /// <summary>
        /// refresh jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<ValidateResponse> RefreshToken(ValidateRefreshToken model)
        {
            var user = await _userService.GetAsync(model.UserId);

            if (!user.RefreshToken.Equals(model.RefreshToken))
                throw new InvalidOperationException("Invalid token");

            return JwtOptions.BuildValidateResponse(user);
        }
    }
}

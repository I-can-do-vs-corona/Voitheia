using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.Geo;
using ActiveCruzer.Startup;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Org.BouncyCastle.Crypto.Tls;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ActiveCruzer.Controllers
{
    /// <summary>
    /// Controller to check if User is logged in
    /// </summary>
    [Route("User")]
    [ApiController]
    public class UserController : BaseController
    {
        private bool disposed = false;

        private UserBLL _userBll;
        private UserManager<User> _userManager;
        private IGeoCodeBll _geoCodeBll;
        private IEmailSenderBll _emailBll;

        private readonly IMapper _mapper;
        private readonly IOptions<Jwt.JwtAuthentication> _jwtAuthentication;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="logger"></param>
        public UserController(IMapper mapper, IOptions<Jwt.JwtAuthentication> jwtAuthentication,
            IConfiguration configuration, UserBLL userBll, UserManager<User> userManager,
            IEmailSenderBll emailBll)
        {
            _geoCodeBll = new GeoCodeBll(mapper, configuration);
            _mapper = mapper;
            _jwtAuthentication = jwtAuthentication;
            _userBll = userBll;
            _userManager = userManager;
            _emailBll = emailBll;
        }

        /// <summary>
        /// Indicates whether or not a User is logged in
        /// </summary>
        /// <remarks>When the code reachis this function the user was successfully logged in through Windows authentication.</remarks>
        /// <returns>Object with RealName and UserName</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [Authorize()]
        [HttpGet]
        [Route("LoggedIn")]
        public ActionResult LoggedIn()
        {
            //If the execution reaches this code the user is logged in.
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<JwtDto>> Register([FromBody] RegisterUserDTO credentials)
        {
            if (ModelState.IsValid)
            {
                var validatedAddress = _geoCodeBll.ValidateAddress(new GeoQuery
                {
                    City = credentials.City, Country = credentials.Country, Street = credentials.Street,
                    Zip = credentials.Zip
                });

                if (validatedAddress.ConfidenceLevel == ConfidenceLevel.High)
                {
                    var result = await _userBll.Register(credentials, validatedAddress.Coordinates);
                    if (result.Succeeded)
                    {
                        User user = await _userBll.GetUser(credentials.Email);
                        var token = GenerateToken(user.UserName, user.Id, null);

                        // email verification 
                        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = "https://voitheia.org/confirmEmail?token=" + emailToken + "?email=" + credentials.Email;
                        //var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new { emailToken, email = user.Email }, Request.Scheme);
                        await _emailBll.SendEmailConfirmationAsync(user.FirstName, user.Email, confirmationLink);


                        return Ok(new JwtDto
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            ValidUntil = token.ValidTo.ToUniversalTime()
                        });
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    return new ContentResult
                    {
                        StatusCode = 424,
                        Content = $"Status Code: {424}; FailedDependency; Address is invalid",
                        ContentType = "text/plain",
                    };
                }
                
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// confirm email with token and email
        /// </summary>
        /// <param name="emailToken"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200"> returns if email was sucessfuly confirmed</response>
        /// <response code="401"> returns if the link or e-mail is not valid</response>
        /// <response code="400"> returns if the email was not able to be confirmed</response>
        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] string emailToken, string email)
        {
            if(emailToken == null | email == null)
            {
                return Unauthorized("The link is not valid.");
            }
            var user = await _userBll.GetUser(email);
            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, emailToken);
                if (result.Succeeded)
                {
                    return Ok("E-Mail successfuly confirmed.");
                }
                return BadRequest();
            }
            return Unauthorized("User not valid.");
        }

        /// <summary>
        /// reset password with token email
        /// </summary>
        /// <param name="forgotPasswordDto"></param>
        /// <returns></returns>
        /// <response code="200"> returns if email mit password reset link was sent</response>
        /// <response code="400"> returns if the input model was not valid (email required)</response>
        /// <response code="404"> returns if the user cannot be found</response>
        [HttpPut]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input is not valid.");
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUri = "https://voitheia.org/resetpassword?token=" + token + "?email=" + forgotPasswordDto.Email;

                await _emailBll.SendEmailPWTokenAsync(user.FirstName, user.Email, callbackUri);
                return Ok("Your password reset was sucessfuly submittet. Please lookup the reset link in your mailbox/ spam folder.");
            }
            return NotFound("User not found.");
        }

        /// <summary>
        /// reset password with given model (email, token, password)
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        /// <response code="200"> returns if password reset was succesful</response>
        /// <response code="400"> returns if the input model was not valid</response>
        /// <response code="404"> returns if the user with the email was not found</response>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task <IActionResult> ResetPassword([FromBody]ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userBll.GetUser(resetPasswordDto.Email);
                if(user != null)
                {
                    await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    return Ok("Password reset sucessful.");
                }
                return NotFound("The user with the email could not be found.");
            }
            return BadRequest("Invalid input.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<JwtDto>> Login([FromBody] CredentialsDTO credentials)
        {
            User user = await _userBll.Login(credentials); 

            if (user != null)
            {
                var token = GenerateToken(user.UserName, user.Id, credentials.MinutesValid);
                return Ok(new JwtDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidUntil = token.ValidTo.ToUniversalTime()
                });
            }

            return Unauthorized();
        }

        /// <summary>
        /// delete current loggedin user account
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> returns if user and related references were sucessful deletet</response>
        /// <response code="401"> returns if user is not logged in</response>
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> DeleteUser()
        {
            var user = await _userBll.GetUserViaId(GetUserId());
            if(user != null)
            {
                var result = await _userBll.DeleteUser(user);
                return Ok(result);
            }
            return Unauthorized("You are not allowed to perform this action.");
        }

        /// <summary>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">User was updated</response>
        /// <response code="401">The logged in user is not updated user</response>
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            var validatedAddress = _geoCodeBll.ValidateAddress(new GeoQuery
            {
                City = user.City,
                Country = user.Country,
                Street = user.Street,
                Zip = user.Zip
            });

            if (validatedAddress.ConfidenceLevel == ConfidenceLevel.High)
            {
                var _user = await _userBll.UpdateUser(user, GetUserId(), validatedAddress.Coordinates);
                if (_user != null)
                {
                    return Ok(_user);
                }

                return Unauthorized("You are not allowed to perform this action.");
            }
            return new ContentResult
            {
                StatusCode = 424,
                Content = $"Status Code: {424}; FailedDependency; Address is invalid",
                ContentType = "text/plain",
            };
        }

        /// <summary>
        /// get user information from logged in user. If 200 returns all went well, otherwise 401 will be retured cause no user is logged in
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            var user = await _userBll.GetUserViaId(GetUserId());
            if(user != null)
            {
                return Ok(_mapper.Map<UserDto>(user));
            }
            return Unauthorized("You are not allowed to perform this action.");
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    if (_userBll != null)
                    {
                        _userBll.Dispose();
                    }
                }

                disposed = true;
            }

            base.Dispose(disposing);
        }


        private JwtSecurityToken GenerateToken(string username, string id, int? credentialsMinutesValid)
        {
            return new JwtSecurityToken(
                audience: _jwtAuthentication.Value.ValidAudience,
                issuer: _jwtAuthentication.Value.ValidIssuer,
                claims: new[]
                {
                    new Claim("id", id),
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                },
                expires: DateTime.UtcNow.AddMinutes(credentialsMinutesValid??1),
                notBefore: DateTime.UtcNow,
                signingCredentials: _jwtAuthentication.Value.SigningCredentials);
        }
    }
}
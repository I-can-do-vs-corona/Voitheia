using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.Error;
using ActiveCruzer.Models.Geo;
using ActiveCruzer.Startup;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="logger"></param>
        public UserController(IMapper mapper,
            IConfiguration configuration, UserBLL userBll, UserManager<User> userManager,
            IEmailSenderBll emailBll)
        {
            _geoCodeBll = new GeoCodeBll(mapper, configuration);
            _mapper = mapper;
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
        [Produces("application/json")]
        public ActionResult LoggedIn()
        {
            //If the execution reaches this code the user is logged in.
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ContactSupportError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(InvalidAddressError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JwtDto>> Register([FromBody] RegisterUserDTO credentials)
        {
            if (ModelState.IsValid)
            {
                var validatedAddress = _geoCodeBll.ValidateAddress(new GeoQuery
                {
                    City = credentials.City, Country = credentials.Country, Street = credentials.Street,
                    Zip = credentials.Zip
                });

                if (validatedAddress.ConfidenceLevel == ConfidenceLevel.High ||
                    validatedAddress.ConfidenceLevel == ConfidenceLevel.Medium)
                {
                    var result = await _userBll.Register(credentials, validatedAddress.Coordinates);
                    if (result.Succeeded)
                    {
                        User user = await _userBll.GetUser(credentials.Email);

                        // email verification 
                        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var queryParams = new Dictionary<string, string>()
                        {
                            {"token", emailToken},
                            {"email", credentials.Email}
                        };
                        var callbackUri =
                            QueryHelpers.AddQueryString("https://voitheia.org/user/confirm-email", queryParams);
                        //var callbackUri = Url.Action(null, "https://voitheia.org/confirm-email", new { emailToken, email = credentials.Email }, Request.Scheme);
                        await _emailBll.SendEmailConfirmationAsync(user.FirstName, user.Email, callbackUri);


                        return Ok(_userBll.GenerateToken(user));
                    }
                    else
                    {
                        return Conflict(new ContactSupportError());
                    }
                }
                else
                {
                    return BadRequest(new InvalidAddressError());
                }
            }
            else
            {
                return BadRequest(new InvalidModelError());
            }
        }

        /// <summary>
        /// confirm email with email and token
        /// </summary>
        /// <param name="confirmationEmailTokenDto"></param>
        /// <returns></returns>
        /// <response code="200"> returns if email was sucessfuly confirmed</response>
        /// <response code="401"> returns if the link or e-mail is not valid</response>
        /// <response code="400"> returns if the email was not able to be confirmed</response>
        [HttpPost]
        [Route("ConfirmEmail")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ContactSupportError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmationEmailTokenDto confirmationEmailTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InvalidModelError());
            }

            var user = await _userBll.GetUser(confirmationEmailTokenDto.email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, confirmationEmailTokenDto.emailToken);
                if (result.Succeeded)
                {
                    return Ok("E-Mail successfuly confirmed.");
                }

                return BadRequest(new ContactSupportError());
            }

            return BadRequest(new ContactSupportError());
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
        [Produces("application/json")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InvalidModelError());
            }

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var queryParams = new Dictionary<string, string>()
                {
                    {"token", token},
                    {"email", forgotPasswordDto.email}
                };
                var callbackUri = QueryHelpers.AddQueryString("https://voitheia.org/user/reset-password", queryParams);
                await _emailBll.SendEmailPWTokenAsync(user.FirstName, user.Email, callbackUri);
                return Ok(new GeneralError(Ok().StatusCode,
                    "Your password reset was sucessfuly submittet. Please lookup the reset link in your mailbox/ spam folder."));
            }

            return Ok(new GeneralError(Ok().StatusCode,
                "Your password reset was sucessfuly submittet. Please lookup the reset link in your mailbox/ spam folder."));
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
        [Produces("application/json")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userBll.GetUser(resetPasswordDto.email);
                if (user != null)
                {
                    await _userManager.ResetPasswordAsync(user, resetPasswordDto.token, resetPasswordDto.password);
                    return Ok("Password reset sucessful.");
                }

                return Ok(new GeneralError(Ok().StatusCode, "Password reset sucessful."));
            }

            return BadRequest(new InvalidModelError());
        }

        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        public async Task<ActionResult<JwtDto>> Login([FromBody] CredentialsDTO credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InvalidModelError());
            }

            var loginResult = await _userBll.Login(credentials);

            switch (loginResult.LoginResult)
            {
                case LoginResult.Sucess:
                    return Ok(_userBll.GenerateToken(loginResult.User));
                case LoginResult.Failed:
                    return Unauthorized(new InvalidCredentialsError());
                case LoginResult.Locked:
                    return Unauthorized(new AccountLockedError());
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        [Produces("application/json")]
        public async Task<ActionResult> DeleteUser()
        {
            var user = await _userBll.GetUserViaId(GetUserId());
            if (user != null)
            {
                var result = await _userBll.DeleteUser(user);
                return Ok(result);
            }

            return Unauthorized(new NotAuthorizedToPerformActionError());
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
        [Produces("application/json")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InvalidModelError());
            }

            var validatedAddress = _geoCodeBll.ValidateAddress(new GeoQuery
            {
                City = user.City,
                Country = user.Country,
                Street = user.Street,
                Zip = user.Zip
            });

            if (validatedAddress.ConfidenceLevel == ConfidenceLevel.High ||
                validatedAddress.ConfidenceLevel == ConfidenceLevel.Medium)
            {
                await _userBll.UpdateUser(user, GetUserId(), validatedAddress.Coordinates);
                return Ok();
            }

            return BadRequest(new InvalidAddressError());
        }

        /// <summary>
        /// change email & username
        /// </summary>
        /// <param name="setNewEmailDto"></param>
        /// <returns></returns>
        /// <response code="200">Email was updated</response>
        /// <response code="401">The logged in user is not updated user</response>
        /// <response code="400">An invalid model was provided</response>
        [Authorize]
        [HttpPut]
        [Route("SetNewEmail")]
        [Produces("application/json")]
        public async Task<IActionResult> ChangeEmail(SetNewEmailDto setNewEmailDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userBll.GetUserViaId(GetUserId());
                if (user != null)
                {
                    if (setNewEmailDto.oldEmail == user.Email)
                    {
                        user.Email = setNewEmailDto.newEmail;
                        user.UserName = setNewEmailDto.newEmail;
                        await _userManager.UpdateAsync(user);
                        return Ok("Email sucessfuly updated");
                    }

                    return Unauthorized(new NotAuthorizedToPerformActionError());
                }

                return Unauthorized(new NotAuthorizedToPerformActionError());
            }

            return BadRequest(new InvalidModelError());
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
        [Produces("application/json")]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            var user = await _userBll.GetUserViaId(GetUserId());
            if (user != null)
            {
                return Ok(_mapper.Map<UserDto>(user));
            }

            return Unauthorized(new NotAuthorizedToPerformActionError());
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateTermsAccepted")]
        [Produces("application/json")]
        public async Task<ActionResult> UpdateTermsAccepted()
        {
            var result = await _userBll.UpdateTermsAccepted(GetUserId());

            if (result.Succeeded)
            {
                return Ok("Succesfuly verified terms.");
            }
            else
            {
                return BadRequest(new TryAgainLaterError());
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteProfilePicture")]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteProfilePicture()
        {
            var userid = GetUserId();

            var result = await _userBll.DeleteProfilePicture(userid);
            if (result.Succeeded)
            {
                return Ok("Profile picture successfully deleted.");
            }

            return BadRequest(new TryAgainLaterError());
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

        /// <summary>
        /// send email confirmation again with given email
        /// </summary>
        /// <param name="confirmationEmailDto"></param>
        /// <returns></returns>
        /// <response code="200"> Confirmation email sucessfuly sent</response>
        /// <response code="400"> Input is invalid, please check your model</response>
        /// <response code="401"> The current user is not logged in</response>
        [HttpPut]
        [Route("SendConfirmationMailAgain")]
        [Produces("application/json")]
        public async Task<IActionResult> SendConfirmationMailAgain(ConfirmationEmailDto confirmationEmailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InvalidModelError());
            }

            var user = await _userBll.GetUser(confirmationEmailDto.Email);
            if (user != null)
            {
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var confirmationLink = "https://voitheia.org/user/confirm-email?token=" + emailToken + "&email=" + user.Email;
                var queryParams = new Dictionary<string, string>()
                {
                    {"token", emailToken},
                    {"email", confirmationEmailDto.Email}
                };
                var confirmationLink =
                    QueryHelpers.AddQueryString("https://voitheia.org/user/confirm-email", queryParams);
                await _emailBll.SendEmailConfirmationAsync(user.FirstName, user.Email, confirmationLink);
                return Ok(new GeneralError(Ok().StatusCode, "Confirmation email sent."));
            }

            return Ok(new GeneralError(Ok().StatusCode, "Confirmation email sent."));
        }

        /// <summary>
        /// change profil picture
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <response code="200"> Profile picture sucessful changed</response>
        /// <response code="401"> The current user is not logged in</response>
        /// <response code="400"> No picture was provided</response>
        [Authorize]
        [HttpPost]
        [Route("ChangeProfilePicture")]
        [Produces("application/json")]
        public async Task<ActionResult> ChangeProfilePicture([FromForm] ProfilePictureDto pictureUpload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InvalidModelError());
            }

            var user = await _userBll.GetUserViaId(GetUserId());
            if (user != null)
            {
                await _userBll.ChangeProfilePicture(pictureUpload.image, GetUserId());
                return Ok("Profile picture succesful changed.");
            }

            return Unauthorized(new NotAuthorizedToPerformActionError());
        }

        /// <summary>
        /// set new password for logged in user
        /// </summary>
        /// <param name="newPasswordDto"></param>
        /// <returns></returns>
        /// <response code="200"> Password sucessfuly changed</response>
        /// <response code="400"> Model is invalid or change process resulted in eror</response>
        [Authorize]
        [HttpPost]
        [Route("SetNewPassword")]
        [Produces("application/json")]
        public async Task<IActionResult> SetNewPassword(NewPasswordDto newPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userBll.GetUserViaId(GetUserId());
                var result =
                    await _userManager.ChangePasswordAsync(user, newPasswordDto.oldPassword,
                        newPasswordDto.newPassword);
                if (result.Succeeded)
                {
                    return Ok("Password sucessfuly changed.");
                }

                return Ok(new GeneralError(Ok().StatusCode, "Password sucessfuly changed."));
            }

            return Unauthorized(new NotAuthorizedToPerformActionError());
            //return BadRequest(new ErrorModel { code = BadRequest().StatusCode, errormessage = "Input is not valid." });
        }
    }
}
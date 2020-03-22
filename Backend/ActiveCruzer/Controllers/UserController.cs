
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ActiveCruzer;
using ActiveCruzer.Controllers;
using ActiveCruzer.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Portal.API.BLL;
using Portal.API.Helper.Authentication;
using Portal.API.Models;
using Portal.API.Models.DTOs;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Portal.API.Controllers
{
    /// <summary>
    /// Controller to check if User is logged in
    /// </summary>
    [Route("api/User")]
    [ApiController]
    public class UserController : BaseController
    {
        private bool disposed = false;

        private UserBLL _BLL = UserBLL.Instance;

        private int _JWTExpirationMinutes = 120;
        private IMapper _mapper;

        private readonly IOptions<JwtAuthentication> _jwtAuthentication;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public UserController(IMapper mapper, IOptions<JwtAuthentication> jwtAuthentication)
        {
            _mapper = mapper;
            _JWTExpirationMinutes = 3600;
            _jwtAuthentication = jwtAuthentication;
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

        // POST api/Account/Register
        [HttpPost]
        [Route("Register")]
        public ActionResult Register([FromBody] RegisterUserDTO credentials)
        {
            if (ModelState.IsValid)
            {
                var result = _BLL.Register(credentials);
                if (result.Success)
                {
                    DateTime expiration = DateTime.Now.AddMinutes(_JWTExpirationMinutes);

                    User user = _BLL.GetUser(credentials.Email);

                   


                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(user.UserName, user.IntId))
                    });
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            else
            {
                return BadRequest();
            }

        }

        
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] CredentialsDTO credentials)
        {
         
            User user = _BLL.Login(credentials); ;
            
            if (user != null)
            {

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(user.UserName, user.IntId))
                });
            }

            return Unauthorized();
        }

        
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    if (_BLL != null)
                    {
                        _BLL.Dispose();
                    }
                }
                disposed = true;
            }

            base.Dispose(disposing);
        }


        private JwtSecurityToken GenerateToken(string username, int id)
        {
            return new JwtSecurityToken(
                audience: "https://localhost:44314/",
                issuer: "https://localhost:44314/",
                claims: new[]
                {
                    new Claim("id", id.ToString()), 
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                },
                expires: DateTime.UtcNow.AddDays(30),
                notBefore: DateTime.UtcNow,
                signingCredentials: _jwtAuthentication.Value.SigningCredentials);
        }
    }
}
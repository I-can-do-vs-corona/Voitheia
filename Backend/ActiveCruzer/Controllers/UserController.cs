using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.Geo;
using ActiveCruzer.Startup;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/User")]
    [ApiController]
    public class UserController : BaseController
    {
        private bool disposed = false;

        private UserBLL _userBll;
        private IGeoCodeBll _geoCodeBll;
        private IMapper _mapper;

        private readonly IOptions<Jwt.JwtAuthentication> _jwtAuthentication;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="logger"></param>
        public UserController(IMapper mapper, IOptions<Jwt.JwtAuthentication> jwtAuthentication,
            IConfiguration configuration, ACDatabaseContext databaseContext)
        {
            _geoCodeBll = new GeoCodeBll(mapper, configuration);
            _mapper = mapper;
            _userBll = new UserBLL(new UserManager(databaseContext),mapper);
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

        [HttpPost]
        [Route("Register")]
        public ActionResult<JwtDto> Register([FromBody] RegisterUserDTO credentials)
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
                    var result = _userBll.Register(credentials, validatedAddress.Coordinates);
                    if (result.Success)
                    {
                        User user = _userBll.GetUser(credentials.Email);
                        var token = GenerateToken(user.UserName, user.Id, null);
                        return Ok(new JwtDto
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            ValidUntil = token.ValidTo.ToUniversalTime()
                        });
                    }
                    else
                    {
                        return BadRequest(result.ErrorMessage);
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


        [HttpPost]
        [Route("Login")]
        public ActionResult<JwtDto> Login([FromBody] CredentialsDTO credentials)
        {
            User user = _userBll.Login(credentials); 

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
        /// delete current loggedin user account. If code 200 returns, user and related references were succesful deleted. If 401 returns, user is not logged in.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteUser()
        {
            var _user = _userBll.GetUserViaId(GetUserId());
            if(_user != null)
            {
                string result = _userBll.DeleteUser(GetUserId());
                return Ok(result);
            }
            return Unauthorized("You are not allowed to perform this action.");
        }

        /// <summary>
        /// Update user. If return code uis 200, the user was updated. If 401 is returned, the user is not the same user as he wants to update(unauthorized)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateUser([FromBody] RegisterUserDTO user)
        {
                var _user = _userBll.UpdateUser(user, GetUserId());
                if (_user != null)
                {
                    return Ok(_user);
                }
                return Unauthorized("You are not allowed to perform this action.");
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
        public ActionResult GetUser()
        {
            var user = _userBll.GetUserViaId(GetUserId());
            if(user != null)
            {
                return Ok(user);
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


        private JwtSecurityToken GenerateToken(string username, int id, int? credentialsMinutesValid)
        {
            return new JwtSecurityToken(
                audience: _jwtAuthentication.Value.ValidAudience,
                issuer: _jwtAuthentication.Value.ValidIssuer,
                claims: new[]
                {
                    new Claim("id", id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                },
                expires: DateTime.UtcNow.AddMinutes(credentialsMinutesValid??1),
                notBefore: DateTime.UtcNow,
                signingCredentials: _jwtAuthentication.Value.SigningCredentials);
        }
    }
}
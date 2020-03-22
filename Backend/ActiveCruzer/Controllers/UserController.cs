using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ActiveCruzer.BLL;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.Geo;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        private UserBLL _userBll = UserBLL.Instance;
        private IGeoCodeBll _geoCodeBll;
        private IMapper _mapper;

        private readonly IOptions<JwtAuthentication> _jwtAuthentication;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public UserController(IMapper mapper, IOptions<JwtAuthentication> jwtAuthentication)
        {
            _mapper = mapper;
            _geoCodeBll = new GeoCodeBll(mapper);
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
            ;

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
                    if (_userBll != null)
                    {
                        _userBll.Dispose();
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
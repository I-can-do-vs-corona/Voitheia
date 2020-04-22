using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Startup;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ActiveCruzer.BLL
{
    ///<Summary>
    /// Business Logic Layer for the History entries
    ///</Summary>
    public class UserBLL
    {
        private bool disposed = false;
        private UserManager<User> _userManager;
        private readonly LoginManager _loginManager;
        private readonly IMapper _mapper;
        private readonly IOptions<Jwt.JwtAuthentication> _jwtAuthentication;

        ///<Summary>
        /// Constructor
        ///</Summary>
        public UserBLL(UserManager<User> userManager, LoginManager loginManager, IMapper mapper,
            IOptions<Jwt.JwtAuthentication> jwtAuthentication)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _mapper = mapper;
            _jwtAuthentication = jwtAuthentication;
        }

        public async Task<IdentityResult> Register(RegisterUserDTO credentials,
            GeoCoordinate validatedAddressCoordinates)
        {
            var user = _mapper.Map<User>(credentials);
            user.Longitude = validatedAddressCoordinates.Longitude;
            user.Latitude = validatedAddressCoordinates.Latitude;
            user.LastLogin = DateTime.UtcNow;
            user.CreatedOn = DateTime.UtcNow;
            user.TermsAccepted = credentials.TermsAccepted;
            return await _userManager.CreateAsync(user, credentials.Password);
        }

        public async Task<LogInResponse> Login(CredentialsDTO credentials)
        {
            return await _loginManager.Login(credentials);
        }


        public async Task<User> GetUser(string eMail)
        {
            return await _userManager.FindByEmailAsync(eMail);
        }


        public async Task<User> GetUserViaId(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> DeleteUser(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> UpdateUser(UpdateUserDto updateUserDto, string userId,
            GeoCoordinate validatedAddressCoordinates)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.Longitude = validatedAddressCoordinates.Longitude;
            user.Latitude = validatedAddressCoordinates.Latitude;
            user.City = updateUserDto.City;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Street = updateUserDto.Street;
            user.Zip = updateUserDto.Zip;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateTermsAccepted(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.TermsAccepted = DateTime.UtcNow;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> IsUserConfirmed(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.EmailConfirmed ?? false;
        }

        public async Task ChangeProfilePicture(IFormFile image, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                user.ProfilPicture = ms.ToArray();
            }

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> OverdueUsersDeleted()
        {
            var users = _userManager.Users.Where(x => x.LastLogin <= DateTime.Today.AddMonths(-6));
            if (users != null)
            {
                foreach (User usr in users)
                {
                    await DeleteUser(usr);
                }

                return true;
            }

            return false;
        }

        public async Task<IdentityResult> DeleteProfilePicture(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            user.ProfilPicture = null;
            return await _userManager.UpdateAsync(user);
        }

        public JwtDto GenerateToken(User user)
        {
            var token = GenerateToken(user.UserName, user.Id);
            return new JwtDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidUntil = token.ValidTo.ToUniversalTime()
            };
        }

        private JwtSecurityToken GenerateToken(string username, string id)
        {
            return new JwtSecurityToken(
                audience: _jwtAuthentication.Value.ValidAudience,
                issuer: _jwtAuthentication.Value.ValidIssuer,
                claims: new[]
                {
                    new Claim("id", id),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                },
                expires: DateTime.UtcNow.AddMinutes(3600),
                notBefore: DateTime.UtcNow,
                signingCredentials: _jwtAuthentication.Value.SigningCredentials);
        }

        ///<Summary>
        /// Function to Dispose the BLL
        ///</Summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    _userManager?.Dispose();
                }

                disposed = true;
            }
        }

        ///<Summary>
        /// Function to Dispose the BLL
        ///</Summary>
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }
    }
}
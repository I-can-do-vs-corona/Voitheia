using System;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.BLL
{
    ///<Summary>
    /// Business Logic Layer for the History entries
    ///</Summary>
    public class UserBLL
    {
        private bool disposed = false;
        private UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        ///<Summary>
        /// Constructor
        ///</Summary>
        public UserBLL(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(RegisterUserDTO credentials,
            GeoCoordinate validatedAddressCoordinates)
        {
            var user = _mapper.Map<User>(credentials);
            user.Longitude = validatedAddressCoordinates.Longitude;
            user.Latitude = validatedAddressCoordinates.Latitude;
            return await _userManager.CreateAsync(user, credentials.Password);
        }

        public async Task<User> Login(CredentialsDTO credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);
            if (user == null)
            {
                return null;
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, credentials.Password);

            return passwordCorrect ? user : null;
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

        public async Task<IdentityResult> UpdateUser(UpdateUserDto updateUserDto, string userId)
        {
            var user = _mapper.Map<User>(updateUserDto);
            user.Id = userId;
            return await _userManager.UpdateAsync(user);
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
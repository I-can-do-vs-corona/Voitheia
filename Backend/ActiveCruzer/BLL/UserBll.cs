using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        ///<Summary>
        /// Constructor
        ///</Summary>
        public UserBLL(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Register(RegisterUserDTO credentials,
            GeoCoordinate validatedAddressCoordinates)
        {
            var user = _mapper.Map<User>(credentials);
            user.Longitude = validatedAddressCoordinates.Longitude;
            user.Latitude = validatedAddressCoordinates.Latitude;
            user.LastLogin = DateTime.Today;
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

        public async Task<IdentityResult> UpdateUser(UpdateUserDto updateUserDto, string userId, GeoCoordinate validatedAddressCoordinates)
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

        public async Task<bool> IsUserConfirmed(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.EmailConfirmed ?? false;
        }

        public async void SetLoginDate(User user)
        {
            user.LastLogin = DateTime.Today;
            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> OverdueUsersDeleted()
        {
            var users =  _userManager.Users.Where(x => x.LastLogin <= DateTime.Today.AddMonths(-6));
            if(users != null)
            {
                foreach (User usr in users)
                {
                    await DeleteUser(usr);
                }
                return true;
            }
            return false;
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
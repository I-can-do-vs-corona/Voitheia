using System;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using AutoMapper;
using GeoCoordinatePortable;

namespace ActiveCruzer.BLL
{
    ///<Summary>
    /// Business Logic Layer for the History entries
    ///</Summary>
    public class UserBLL
    {
        private bool disposed = false;
        private IUserManager _userManager;
        private readonly IMapper _mapper;

        ///<Summary>
        /// Constructor
        ///</Summary>
        public UserBLL(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        ///<Summary>
        /// Function to Save changed data. IS NOT USED!
        ///</Summary>
        public void Save()
        {
        }


        public RegisteringResult Register(RegisterUserDTO credentials, GeoCoordinate validatedAddressCoordinates)
        {
            var user = _mapper.Map<User>(credentials);
            user.Longitude = validatedAddressCoordinates.Longitude;
            user.Latitude = validatedAddressCoordinates.Latitude;
            var result = _userManager.CreateUser(user, credentials.Password);

            if (result.Success)
            {
                return result;
            }

            throw new Exception(result.ErrorMessage);
        }

        public User Login(CredentialsDTO credentials)
        {
            return _userManager.CheckPassword(credentials.Email, credentials.Password)
                ? _userManager.FindByUserName(credentials.Email)
                : null;
        }


        public User GetUser(string eMail)
        {
            return _userManager.FindByUserName(eMail);
        }


        public User GetUserViaId(in int userId)
        {
            return _userManager.FindById(userId);
        }

        public string DeleteUser(int userId)
        {
            return _userManager.DeleteUser(userId);
        }

        public User UpdateUser(User user)
        {
            return _userManager.UpdateUser(user);
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
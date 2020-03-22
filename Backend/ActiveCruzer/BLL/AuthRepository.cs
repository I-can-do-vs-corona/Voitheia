using System;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;

namespace ActiveCruzer.BLL
{
    public class AuthRepository : IDisposable
    {
        private bool disposed = false;


        private IUserManager _userManager = new UserManager();


        public RegisteringResult Register(RegisterUserDTO credentials)
        {
            User user = new User
            {
                UserName = credentials.Email,
                Email = credentials.Email,
                Street = credentials.Street,
                City = credentials.City,
                Zip = credentials.Zip,
                FirstName = credentials.FirstName,
                LastName = credentials.LastName
            };

            var result = _userManager.CreateUser(user, credentials.Password);

            return result;
        }

        public User Login(CredentialsDTO credentials)
        {
            if (credentials != null && !string.IsNullOrWhiteSpace(credentials.Email) && !string.IsNullOrWhiteSpace(credentials.Password))
            {
                var user = FindUser(credentials.Email);
                if (user != null)
                {
                    var result =  _userManager.CheckPassword(user, credentials.Password);
                    if (result)
                    {
                        return user;
                    }
                    throw new Exception("UnableToLogin");
                }
                return null;
            }
            throw new Exception("CredentialsNotPresented");
        }


        public User FindUser(string userName)
        {
            return _userManager.FindByUserName(userName);
        }

        

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    if (_userManager != null)
                    {
                        _userManager.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
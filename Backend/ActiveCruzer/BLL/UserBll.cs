using System;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using GeoCoordinatePortable;

namespace ActiveCruzer.BLL
{
    ///<Summary>
    /// Business Logic Layer for the History entries
    ///</Summary>
    public class UserBLL
    {
        private static UserBLL instance = null;
        private static readonly object padlock = new object();

        public static UserBLL Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserBLL();
                    }

                    return instance;
                }
            }
        }

        private bool disposed = false;

        internal static AuthRepository _AuthRepository;

        ///<Summary>
        /// Constructor
        ///</Summary>
        private UserBLL()
        {
            _AuthRepository = new AuthRepository();

            _AuthRepository.Register(new RegisterUserDTO
            {
                Email = "first@test.com",
                Street = "Neubessinger Str. 16",
                Zip = "97535",
                City = "Wasserlosen",
                Country = "Germany",
                Password = "first"
            }, new GeoCoordinate(50.054710, 9.995050));

            _AuthRepository.Register(new RegisterUserDTO
            {
                Email = "test@test.com",
                Street = "Georg-Schäfer-Straße 30",
                Zip = "97421",
                City = "Schweinfurt",
                Country = "Germany",
                Password = "test"
            }, new GeoCoordinate(50.102590, 10.795760));

        }

        ///<Summary>
        /// Function to Save changed data. IS NOT USED!
        ///</Summary>
        public void Save()
        {

        }


        public RegisteringResult Register(RegisterUserDTO credentials, GeoCoordinate validatedAddressCoordinates)
        {

            var result = _AuthRepository.Register(credentials, validatedAddressCoordinates);

            if (result.Success)
            {
                return result;
            }
           
            throw new Exception("ErrorInRegistration");
        }

        public User Login(CredentialsDTO credentials)
        {
            return _AuthRepository.Login(credentials);
        }


        public User GetUser(string eMail)
        {
            return _AuthRepository.FindUser(eMail);
        }


        public User GetUserViaId(in int userId)
        {
            return _AuthRepository.FindUser(userId);
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
                    if (_AuthRepository != null)
                    {
                        _AuthRepository.Dispose();
                    }
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
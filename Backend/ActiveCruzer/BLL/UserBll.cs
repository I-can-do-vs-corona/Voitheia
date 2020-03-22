using System;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;

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
                Email = "test@test.com",
                Password = "test"
            });
        }

        ///<Summary>
        /// Function to Save changed data. IS NOT USED!
        ///</Summary>
        public void Save()
        {

        }


        public RegisteringResult Register(RegisterUserDTO credentials)
        {

            var result = _AuthRepository.Register(credentials);

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


        //internal string RandomString(int length)
        //{
        //    const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //    System.Text.StringBuilder res = new System.Text.StringBuilder();
        //    using (System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        //    {
        //        byte[] uintBuffer = new byte[sizeof(uint)];

        //        while (length-- > 0)
        //        {
        //            rng.GetBytes(uintBuffer);
        //            uint num = BitConverter.ToUInt32(uintBuffer, 0);
        //            res.Append(valid[(int)(num % (uint)valid.Length)]);
        //        }
        //    }

        //    return res.ToString();
        //}
    }
}
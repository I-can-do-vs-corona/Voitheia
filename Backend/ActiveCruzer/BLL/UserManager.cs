using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using ActiveCruzer.Models;
using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.BLL
{
    public class UserManager : IUserManager
    {
        Dictionary<string, User> _users = new Dictionary<string, User>();

        private int Id = 0;
        private readonly MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
        public void Dispose()
        {
        }

        public RegisteringResult CreateUser(User user, string credentialsPassword)
        {
            if (_users.ContainsKey(user.UserName))
            {
                return new RegisteringResult{Success = false, ErrorMessage = "User already exists"};
            }

            ;
            user.PasswordHash = GetHash(credentialsPassword);
            Interlocked.Increment(ref Id);
            user.IntId = Id;
            _users.Add(user.UserName,user);
            return new RegisteringResult{Success = true};
        }

        public bool CheckPassword(User user, string credentialsPassword)
        {
            var hash = GetHash(credentialsPassword);
            return _users[user.UserName].PasswordHash == hash;
        }


        public User FindByUserName(string userName)
        {
            return _users[userName];
        }

        public User FindById(int userId)
        {
            return _users.Values.First(it => it.IntId == userId);
        }

        private string GetHash(string password)
        {
            return Encoding.ASCII.GetString(_md5.ComputeHash(Encoding.ASCII.GetBytes(password)));
        }
    }
}
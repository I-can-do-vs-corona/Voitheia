using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.BLL
{
    public class UserManager : IUserManager
    {
        private readonly ACDatabaseContext _databaseContext;
        private PasswordHasher<User> _passwordHasher;

        public UserManager(ACDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _passwordHasher = new PasswordHasher<User>();
        }

        public RegisteringResult CreateUser(User user, string credentialsPassword)
        {
            if (_databaseContext.Users.Any(it => it.NormalizedUserName == user.NormalizedUserName))
            {
                return new RegisteringResult {ErrorMessage = "User with username already exists", Success = false};
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, credentialsPassword);
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
            return new RegisteringResult {Success = true};
        }

        public bool CheckPassword(string username, string credentialsPassword)
        {
            var user = _databaseContext.Users.FirstOrDefault(it => it.NormalizedUserName == username.ToLower());
            if (user == null) return false;
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, credentialsPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                return false;
            }

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, credentialsPassword);
                _databaseContext.Update(user);
                _databaseContext.SaveChanges();
            }

            if (result == PasswordVerificationResult.Success)
            {
                return true;
            }

            return false;
        }

        public User FindByUserName(string userName)
        {
            return _databaseContext.Users.FirstOrDefault(it => it.NormalizedUserName == userName.ToLower());
        }

        public User FindById(int userId)
        {
            return _databaseContext.Users.Find(userId);
        }

        public string DeleteUser(int userId)
        {
            var user = _databaseContext.Users.FirstOrDefault(usr => usr.Id == userId);
            if(user != null)
            {
                var requests = _databaseContext.Request.Where(req => req.Volunteer == userId);
                if(requests != null)
                {
                    foreach (Request req in requests)
                    {
                        req.Volunteer = null;
                    }
                    _databaseContext.UpdateRange(requests);
                }
                _databaseContext.Remove(user);
                _databaseContext.SaveChanges();
                return user.Email;
            }
            return null;
        }

        public User UpdateUser(User user)
        {
            var _user = _databaseContext.Users.FirstOrDefault(usr => usr.Id == user.Id);
            if(_user != null)
            {
                _user.Street = user.Street;
                _user.City = user.City;
                _user.Zip = user.Zip;
                _user.Email = user.Email;
                _user.EmailConfirmed = user.EmailConfirmed;
                _databaseContext.Users.Update(_user);
                _databaseContext.SaveChanges();
                return _user;
            }
            return null;
        }

        public void Dispose()
        {
        }
    }
}
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

        /// <summary>
        /// register user in db and return result
        /// </summary>
        /// <param name="user"></param>
        /// <param name="credentialsPassword"></param>
        /// <returns></returns>
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

        /// <summary>
        /// check password with given user in db
        /// </summary>
        /// <param name="username"></param>
        /// <param name="credentialsPassword"></param>
        /// <returns></returns>
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

        /// <summary>
        /// find user in db by username and return user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindByUserName(string userName)
        {
            return _databaseContext.Users.FirstOrDefault(it => it.NormalizedUserName == userName.ToLower());
        }

        /// <summary>
        /// find user by userid and return it
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User FindById(int userId)
        {
            return _databaseContext.Users.Find(userId);
        }

        /// <summary>
        /// delete user account with param user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string DeleteAccount(int userId)
        {
            var _user = _databaseContext.Users.FirstOrDefault(x=> x.Id == userId);
            if( _user != null)
            {
                var requests = _databaseContext.Request.Where(x => x.Volunteer == userId);
                if (requests != null)
                {
                    foreach (Request request in requests)
                    {
                        request.Volunteer = null;
                        if (request.Status != Request.RequestStatus.Closed)
                        {
                            request.Status = Request.RequestStatus.Open;
                        }
                    }
                    _databaseContext.UpdateRange(requests);
                }
                _databaseContext.Remove(_user);
                _databaseContext.SaveChanges();
                return _user.UserName;
            }
            return null;
        }

        /// <summary>
        /// dispose connection db
        /// </summary>
        public void Dispose()
        {
        }
    }
}
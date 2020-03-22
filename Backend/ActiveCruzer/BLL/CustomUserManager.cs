using System;
using ActiveCruzer.Models;

namespace ActiveCruzer.BLL
{
    public interface IUserManager : IDisposable
    {
        RegisteringResult CreateUser(User user, string credentialsPassword);
        bool CheckPassword(User user, string credentialsPassword);
        User FindByUserName(string userName);
    }

    public class RegisteringResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
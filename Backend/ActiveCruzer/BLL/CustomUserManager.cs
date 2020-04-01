using System;
using ActiveCruzer.Models;

namespace ActiveCruzer.BLL
{
    public interface IUserManager : IDisposable
    {
        RegisteringResult CreateUser(User user, string credentialsPassword);
        bool CheckPassword(string username, string credentialsPassword);
        User FindByUserName(string userName);
        User FindById(int userId);
        string DeleteAccount(User user);
    }

    public class RegisteringResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
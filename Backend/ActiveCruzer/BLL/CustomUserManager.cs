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
        string DeleteUser(int id);
        RegisteringResult UpdateUser(User user, string credentialsPassword, int id);
    }

    public class RegisteringResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Portal.API.Models;

namespace Portal.API.Helper.Authentication
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
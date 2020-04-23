using System.Dynamic;

namespace ActiveCruzer.Models
{
    public class LogInResponse
    {
        public User User { get; set; }

        public LoginResult LoginResult { get; set; }
    }

    public enum LoginResult
    {
        Sucess,
        Failed,
        Locked
    }
}
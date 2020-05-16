using System;
using System.Threading.Tasks;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.BLL
{
    public class LoginManager
    {
        private readonly UserManager<User> _userManager;

        public LoginManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<LogInResponse> Login(CredentialsDTO credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);
            if (user == null)
            {
                return new LogInResponse {LoginResult = LoginResult.Failed};
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return new LogInResponse {LoginResult = LoginResult.Locked, User = user};
            }

            if (await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                return await LogUserIn(user);
            }

            return await HandleInvalidPassword(user);
        }

        private async Task<LogInResponse> HandleInvalidPassword(User user)
        {
            user.AccessFailedCount += 1;
            await _userManager.UpdateAsync(user);

            if (user.AccessFailedCount <= 3)
            {
                return new LogInResponse { LoginResult = LoginResult.Failed };
            }
            else
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));
                return new LogInResponse {LoginResult = LoginResult.Locked};
            }
        }

        private async Task<LogInResponse> LogUserIn(User user)
        {
            user.LastLogin = DateTime.UtcNow;
            user.AccessFailedCount = 0;
            await _userManager.UpdateAsync(user);
            return new LogInResponse
            {
                User = user, LoginResult = LoginResult.Sucess
            };
        }
    }
}
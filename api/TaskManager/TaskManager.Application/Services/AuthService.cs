using System;
using System.Text;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Request;
using TaskManager.Domain.Models.Response;

namespace TaskManager.Application.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private string _messageAuthenticationFailed = "Authentication failed. Wrong user or password.";

        public AuthService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public AuthResponse Get(AuthRequest auth)
        {
            if (this.isAuthenticated(auth))
                return _jwtService.Get(auth);

            return new AuthResponse
            {
                Success = false,
                Token = null
            };
        }

        private bool isAuthenticated(AuthRequest auth)
        {
            User authUser = _userService.GetAuthUser(auth);
            if (authUser == null)
            {
                AddNotification(_messageAuthenticationFailed);
                return false;
            }

            string password = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authUser.Password));
            if (password != auth.Password)
            {
                AddNotification(_messageAuthenticationFailed);
                return false;
            }

            return true;
        }
    }
}

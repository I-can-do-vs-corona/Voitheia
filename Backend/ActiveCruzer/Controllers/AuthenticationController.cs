using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ActiveCruzer.Models;

namespace ActiveCruzer.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        // action route /Login
        public string Login()
        {
            // TODO
            return "TODO: logic for login";
        }

        //action route /Logout
        public string Logout()
        {
            // TODO
            return "TODO: logic for logout";
        }

    }
}

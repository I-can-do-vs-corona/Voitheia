using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase, IDisposable
    {
        public void Dispose()
        {
        }

        protected string GetUserId()
        {
            var claims = User.Claims;
            return User.Claims.FirstOrDefault(c => c.Type == "id").Value;
        }

        protected void Dispose(in bool disposing)
        {
        }
    }
}
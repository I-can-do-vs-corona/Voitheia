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
            var id = User.Claims.FirstOrDefault(c => c.Type == "id").Value;
            if (id == null)
            {
                throw new Exception("Error getting userid");
            }

            return id;
        }

        protected void Dispose(in bool disposing)
        {
        }
    }
}
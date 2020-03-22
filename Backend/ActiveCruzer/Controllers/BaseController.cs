using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.Controllers
{
    [ApiController]
    public class BaseController :ControllerBase, IDisposable 
    {
        public void Dispose()
        {
        }

        protected int GetUserId()
        {
            try
            {
                var claims = User.Claims;
                return Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            }
            catch (Exception)
            {
                return 1;
            }
            
        }
        protected void Dispose(in bool disposing)
        {

        }
    }
}
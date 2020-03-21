using System;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.Controllers
{
    [ApiController]
    public class BaseController :ControllerBase, IDisposable 
    {
        public void Dispose()
        {
        }

        protected void Dispose(in bool disposing)
        {

        }
    }
}
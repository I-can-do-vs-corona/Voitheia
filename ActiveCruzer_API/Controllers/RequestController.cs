using Microsoft.AspNetCore.Mvc;
using ActiveCruzer.Models;
using Microsoft.Extensions.Logging;

namespace ActiveCruzer_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<RequestController> _logger;

        public RequestController(DatabaseContext context, ILogger<RequestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public void InsertRequest()
        {

        }
    }
}

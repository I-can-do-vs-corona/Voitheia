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
    public class RequestController : Controller
    {
        private readonly ILogger<RequestController> _logger;

        public RequestController(ILogger<RequestController> logger)
        {
            _logger = logger;
        }

        // action vor route /InsertInDB
        public string InsertInDB()
        {
            // TODO
            return "TODO: logic for inserting";
        }

        //action for route /LookUpInDB
        public string LookUpInDB()
        {
            // TODO
            return "TODO: logic for lookup in db ";
        }
    }
}

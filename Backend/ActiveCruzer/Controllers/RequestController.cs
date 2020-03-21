using System.Linq;
using System.Net;
using System.Net.Http;
using ActiveCruzer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActiveCruzer.Controllers
{
    
    public class DatabaseController : Controller
    {
        // configs must be generated and connection must be parsed
        private static DbContextOptions<ACDatabaseContext> options;
        private ACDatabaseContext _db = new ACDatabaseContext(options);

        /// <summary>
        /// Inserts a request to the database
        /// </summary>
        /// <returns></returns>
        [HttpPost("/requests/")]
        public string InsertRequest([FromBody]Request req)
        {
            if (ModelState.IsValid)
            {
                _db.Request.Add(req);
                _db.SaveChanges();
            }
            else
            {
                return "Bad request";
            }
            return "request processed";
        }

        /// <summary>
        /// Removes a request from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/requests/{id}")]
        public string RemoveRequest([FromRoute]int id)
        {
            if(ModelState.IsValid)
            {
                Request tmp = _db.Request.Find(id);
                _db.Remove(tmp);
                _db.SaveChanges();
            }
            else
            {
                return "Bad request";
            }
            return "request processed";
        }
        /// <summary>
        /// Updates the status of a request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch("/requests/{id}")]
        public string ChangeStatus([FromRoute]int id, [FromBody] Request.RequestStatus status)
        {
            if(ModelState.IsValid)
            {
                
            }
            else
            {

            }
            return "request processed";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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

        public string InsertRequest(Request req)
        {
            if (ModelState.IsValid)
            {
                _db.Request.Add(req);
                _db.SaveChanges();
            }
            else
            {
                return HttpStatusCodeResult(HttpStatusCode.BadRequest, "No valid type of Request was retrieved");
            }
            return "request processed";
        }

        public string RemoveRequest(Request req)
        {
            if(ModelState.IsValid)
            {
                Request tmp = _db.Request.Find(req.unique_id);
                _db.Remove(tmp);
                _db.SaveChanges();
            }
            else
            {
                return HttpStatusCodeResult(HttpStatusCode.BadRequest, "No valid type of Request was retrieved");
            }
            return "request processed";
        }

        public string ChangeStatus(Request req, int changeNum)
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

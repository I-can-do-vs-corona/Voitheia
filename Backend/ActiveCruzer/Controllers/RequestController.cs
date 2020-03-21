using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActiveCruzer.Controllers
{
    public class DatabaseController : ControllerBase, IDisposable
    {
        // configs must be generated and connection must be parsed
        private static DbContextOptions<ACDatabaseContext> options;
        private ACDatabaseContext _db = new ACDatabaseContext(options);

        /// <summary>
        /// Inserts a request to the database
        /// </summary>
        /// <returns></returns>
        [HttpPost("/requests/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> InsertRequest([FromBody] Request req)
        {
            if (ModelState.IsValid)
            {
                _db.Request.Add(req);
                _db.SaveChanges();
            }
            else
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetById), new {id = req.unique_id}, req);
        }

        /// <summary>
        /// Removes a request from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/requests/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult RemoveRequest([FromRoute] int id)
        {
            var request = _db.Request.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            _db.Remove(request);
            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates the status of a request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch("/requests/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string ChangeStatus([FromRoute] int id, [FromBody] Request.RequestStatus status)
        {
            throw new NotImplementedException();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Request> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
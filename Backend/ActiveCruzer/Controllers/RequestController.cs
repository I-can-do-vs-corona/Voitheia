using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.DTO.Request;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActiveCruzer.Controllers
{
    /// <summary>
    /// Database controller for transactions related to the database
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RequestController : BaseController
    {
        private readonly IRequestBll _bll = MemoryRequestBll.Instance;
        private IMapper _mapper;
        private bool _disposed;

        public RequestController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Inserts a request to the database
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CreateRequestResponseDto> InsertRequest([FromBody] CreateRequestDto req)
        {
            if (ModelState.IsValid)
            {
                var request = _mapper.Map<Request>(req);
                request.Status = Models.Request.RequestStatus.Open;
                var id = _bll.CreateRequest(request);
                return CreatedAtAction(nameof(GetById), new {id}, new CreateRequestResponseDto {Id = id});
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Removes a request from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult RemoveRequest([FromRoute] int id)
        {
            if (_bll.Exists(id))
            {
                _bll.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Get request by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetRequestResponse> GetById(int id)
        {
            var request = _bll.GetRequest(id);

            return Ok(new GetRequestResponse
            {
                Request = _mapper.Map<RequestDto>(request)
            });
        }

        /// <summary>
        /// Get all requests in a specific area if longitude and latitude is not given,
        /// the registered address of the user is taken
        /// </summary>
        /// <param name="longitude">Longitude in degrees, if this is not passed, the users address is taken</param>
        /// <param name="latitude">Latitude in degrees, if this is not passed, the users address is taken</param>
        /// <param name="amount">How many requests to retrieve</param>
        /// <param name="metersPerimeter">Which perimeter should be kept in considoration</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<GetAllRequestResponse> GetAll([FromQuery] double longitude,
            [FromQuery] double latitude, [FromQuery] int amount = 10, [FromQuery] int metersPerimeter = 2000)
        {
            var requests = _bll.GetRequestsViaGps(latitude, longitude, amount, metersPerimeter);
            var dtoRequests = requests.Select(it => _mapper.Map<RequestDto>(it)).ToList();

            return Ok(new GetAllRequestResponse {Requests = dtoRequests});
        }

        /// <summary>
        /// IDisposible for connections
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _bll?.Dispose();
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
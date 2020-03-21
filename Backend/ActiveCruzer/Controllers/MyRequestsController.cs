using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.BLL;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO.MyRequests;
using ActiveCruzer.Models.DTO.Request;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.Controllers
{
    /// <summary>
    /// This controller is used to handle requests on "Volunteer level".
    /// Via this controller a volunteer can take a request and get all requests from him
    /// For this controller authentication is required
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MyRequestsController: BaseController
    {
        private readonly IMyRequestsBll _bll = MemoryRequestBll.Instance;
        private IMapper _mapper;
        private bool _disposed;
        private int _hardcodedUser = 1;

        public MyRequestsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Add a request to the users personal request list.
        /// </summary>
        /// <param name="takeRequestDto">The required data to take a request. Right now only the request id</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult TakeRequest([FromBody] TakeRequestDto takeRequestDto)
        {
            var userId = _hardcodedUser;
            if (ModelState.IsValid)
            {
                if (_bll.Exists(takeRequestDto.RequestId))
                {
                    var id = _bll.TakeRequest(takeRequestDto.RequestId, _hardcodedUser);
                    return CreatedAtAction(nameof(GetById), new { id }, new CreateRequestResponseDto { Id = id });
                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get a specific request assigned to the logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetRequestResponse> GetById(int id)
        {
            if (_bll.ExistsOnUser(id,_hardcodedUser))
            {
                var request = _bll.GetRequest(id, _hardcodedUser);

                return Ok(new GetRequestResponse
                {
                    Request = _mapper.Map<RequestDto>(request)
                });
            }
            else
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// Cancel a previously taken request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult RemoveRequest([FromRoute] int id)
        {
            if (_bll.ExistsOnUser(id,_hardcodedUser))
            {
                if (_bll.IsNotClosed(id))
                {
                    _bll.AbortRequest(id, _hardcodedUser);
                    return Ok();
                }
                else
                {
                    return BadRequest("The request is already closed");
                }
                
            }
            else
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Get all requests assigned to the logged in user
        /// </summary>
        /// <param name="longitude">Longitude in degrees</param>
        /// <param name="latitude">Latitude in degrees</param>
        /// <param name="amount">How many requests to retrieve</param>
        /// <param name="metersPerimeter">Which perimeter should be kept in considoration</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<GetAllRequestResponse> GetAll()
        {
            var requests = _bll.GetAllFromUser(_hardcodedUser);
            var dtoRequests = requests.Select(it => _mapper.Map<RequestDto>(it)).ToList();

            return Ok(new GetAllRequestResponse { Requests = dtoRequests });
        }

        /// <summary>
        /// Updates the status of a request. Currently only close is supported
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PatchRequest([FromRoute] int id, [FromBody] PatchRequestDto patchRequest)
        {
            if (ModelState.IsValid)
            {
                if (_bll.ExistsOnUser(id, _hardcodedUser))
                {
                    _bll.FinishRequest(id, _hardcodedUser);
                    return Ok();
                }
                else
                {
                    return NotFound(id);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}

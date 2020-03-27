using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO.MyRequests;
using ActiveCruzer.Models.DTO.Request;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private UserBLL _userBll = UserBLL.Instance;
        private readonly IMyRequestsBll _requestBll;

        private IMapper _mapper;
        private bool _disposed;

        /// <summary>
        /// base constructor for myrequest controller
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="myRequestBll"></param>
        public MyRequestsController(IMapper mapper, IMyRequestsBll myRequestBll)
        {
            _mapper = mapper;
            _requestBll = myRequestBll;
        }

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
            var userId = GetUserId();
            if (ModelState.IsValid)
            {
                if (_requestBll.Exists(takeRequestDto.RequestId))
                {
                    var id = _requestBll.TakeRequest(takeRequestDto.RequestId, userId);
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
            var userId = GetUserId();
            if (_requestBll.ExistsOnUser(id, userId))
            {
                var request = _requestBll.GetRequest(id);

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
            var userId = GetUserId();
            if (_requestBll.ExistsOnUser(id, userId))
            {
                if (_requestBll.IsNotClosed(id))
                {
                    _requestBll.AbortRequest(id, userId);
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
            var requests = _requestBll.GetAllPendingFromUser(GetUserId());
            var dtoRequests = requests.Select(it => _mapper.Map<RequestDto>(it)).ToList();

            return Ok(new GetAllRequestResponse { Requests = dtoRequests, TotalCount = dtoRequests.Count});
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
            var userId = GetUserId();
            if (ModelState.IsValid)
            {
                if (_requestBll.ExistsOnUser(id, userId))
                {
                    _requestBll.FinishRequest(id);
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

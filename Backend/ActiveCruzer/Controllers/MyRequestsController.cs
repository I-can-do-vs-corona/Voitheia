﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO.MyRequests;
using ActiveCruzer.Models.DTO.Request;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
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
    [Authorize]
    [Route("[controller]")]
    public class MyRequestsController : BaseController
    {
        private readonly IMyRequestsBll _requestBll;

        private IMapper _mapper;
        private bool _disposed;
        private readonly UserBLL _userBll;

        /// <summary>
        /// base constructor for myrequest controller
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="myRequestBll"></param>
        public MyRequestsController(IMapper mapper, IMyRequestsBll myRequestBll, UserBLL userBll)
        {
            _mapper = mapper;
            _requestBll = myRequestBll;
            _userBll = userBll;
        }

        /// <summary>
        /// Add a request to the users personal request list.
        /// </summary>
        /// <param name="takeRequestDto">The required data to take a request. Right now only the request id</param>
        /// <returns></returns>
        /// <response code="403">The users email is not confirmed</response>
        /// <response code="404">The request with the provided ID does not exist</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> TakeRequest([FromBody] TakeRequestDto takeRequestDto)
        {
            var userId = GetUserId();
            if (!await _userBll.IsUserConfirmed(userId))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                if (_requestBll.Exists(takeRequestDto.RequestId))
                {
                    var id = _requestBll.TakeRequest(takeRequestDto.RequestId, userId);
                    return CreatedAtAction(nameof(GetById), new {id}, new CreateRequestResponseDto {Id = id});
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
        /// <response code="403">The users email is not confirmed</response>
        /// <response code="404">The request with the provided ID does not exist</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetRequestResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRequestResponse>> GetById(int id)
        {
            var userId = GetUserId();
            if (!await _userBll.IsUserConfirmed(userId))
            {
                return Forbid();
            }

            if (_requestBll.ExistsOnUser(id, userId))
            {
                var request = _requestBll.GetRequest(id, userId);

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
        /// <response code="200">The request was successfully removed</response>
        /// <response code="400">The request is already closed and can not be removed</response>
        /// <response code="403">The users email is not confirmed</response>
        /// <response code="404">The request with the provided ID does not exist</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveRequest([FromRoute] int id)
        {
            var userId = GetUserId();
            if (!await _userBll.IsUserConfirmed(userId))
            {
                return Forbid();
            }

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
        /// <response code="403">The users email is not confirmed</response>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetAllRequestResponse>> GetAll()
        {
            var userId = GetUserId();
            if (!await _userBll.IsUserConfirmed(userId))
            {
                return Forbid();
            }

            var requests = _requestBll.GetAllPendingFromUser(userId);
            return Ok(new GetAllMyRequestResponse { Requests = requests, TotalCount = requests.Count});
        }

        /// <summary>
        /// Updates the status of a request. Currently only close is supported
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="403">The users email is not confirmed</response>
        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PatchRequest([FromRoute] int id, [FromBody] PatchRequestDto patchRequest)
        {
            var userId = GetUserId();

            if (!await _userBll.IsUserConfirmed(userId))
            {
                return Forbid();
            }

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
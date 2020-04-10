using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.Models.DTO.Request;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.BLL
{
    public class MyRequestBll : IMyRequestsBll
    {
        private readonly IMapper _mapper;
        private readonly ACDatabaseContext _context;
        private UserManager<User> _userManager;

        /// <summary>
        /// basic constructur for MyRequests
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        public MyRequestBll(IMapper mapper, UserManager<User> usermanager,ACDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// take request with id param and current user id param
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int TakeRequest(int requestId, string userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                request.Volunteer = userId;
                request.Status = Request.RequestStatus.Pending;
                _context.Update(request);
                _context.SaveChanges();
                return request.Id;
            }

            return 0;
        }

        /// <summary>
        /// finish request with request id and user id param
        /// </summary>
        /// <param name="requestId"></param>
        public void FinishRequest(int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                request.Status = Request.RequestStatus.Closed;
                _context.Update(request);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// abort request with user id and request id
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        public void AbortRequest(int requestId, string userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                request.Status = Request.RequestStatus.Open;
                request.Volunteer = null;
                _context.Update(request);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// return request with given request id
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RequestDto GetRequest(int requestId, string userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            var user = _userManager.FindByIdAsync(userId).Result;
            var userCoordinate = new GeoCoordinate(user.Latitude, user.Longitude);

            if (request != null)
            {
                return MapRequestAndCalculateDistance(request, userCoordinate);
            }

            return null;
        }

        /// <summary>
        /// dispose from inherit
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// check if request exsists on user with param user id and request id
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ExistsOnUser(int requestId, string userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                return request.Id == requestId && request.Volunteer == userId;
            }

            return false;
        }

        /// <summary>
        /// get all reqeusts of user based on hardcoded user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RequestDto> GetAllPendingFromUser(string userId)
        {
            var requests = _context.Request
                .Where(it => it.Volunteer == userId && it.Status == Request.RequestStatus.Pending).ToList();
            var user = _userManager.FindByIdAsync(userId).Result;
            var userCoordinate = new GeoCoordinate(user.Latitude, user.Longitude);

            return requests.Select(it => MapRequestAndCalculateDistance(it, userCoordinate)).ToList();
        }

        /// <summary>
        /// does requests exist bool?
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public bool Exists(int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// reuquest not closed bool?
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public bool IsNotClosed(int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                if(request.Status != Request.RequestStatus.Closed)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        private RequestDto MapRequestAndCalculateDistance(Request request, GeoCoordinate userCoordinate)
        {
            var mapped = _mapper.Map<RequestDto>(request);
            mapped.DistanceToUser =
                (int)userCoordinate.GetDistanceTo(new GeoCoordinate(request.Latitude, request.Longitude));
            return mapped;
        }
    }
}
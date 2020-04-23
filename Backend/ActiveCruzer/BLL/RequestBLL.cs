using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Helper;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO.Request;
using AutoMapper;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.BLL
{
    public class RequestBll : IRequestBll
    {
        private readonly IMapper _mapper;
        private readonly ACDatabaseContext _context;

        public RequestBll(IMapper mapper, ACDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// create request in db from request input
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int CreateRequest(Request request, string userId)
        {
            var _request = request;
            if(userId != null)
            {
                request.CreatedBy = userId;
                _context.Request.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            request.CreatedBy = null;
            _context.Request.Add(request);
            _context.SaveChanges();
            return request.Id;
        }


        /// <summary>
        /// try lookup request in db and return result bool
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            var request = _context.Request.Where(x => x.Id == id);
            if (request != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// delete request in database related to id param
        /// </summary>
        /// <param name="i"></param>
        public void Delete(int id)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == id);
            if (request != null)
            {
                _context.Remove(request);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// update status of request related to id param
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void UpdateStatus(int id, Request.RequestStatus status)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == id);
            if (request != null)
            {
                request.Status = status;
                _context.Update(request);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// get request from database and return it else null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Request GetRequest(int id)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == id);
            if(request != null)
            {
                return request;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// update request volunteer related to id of volunteer and request id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="requestId"></param>
        public void UpdateAssignee(string userId, int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
            {
                request.Volunteer = userId;
                _context.Update(request);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// get list of requests in current viereck
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="amount"></param>
        /// <param name="metersPerimeter"></param>
        /// <returns></returns>
        public List<Request> GetRequestsViaGps(GeoCoordinate coordinates, int amount, int metersPerimeter)
        {
            var degrees = GeoHelper.MetersToDegree(metersPerimeter);
            var northernMax = coordinates.Latitude + degrees;
            var southernMax = coordinates.Latitude - degrees;
            var westernMax = coordinates.Longitude - degrees;
            var easternMax = coordinates.Longitude + degrees;

            return _context.Request.Where(it => it.Status == Request.RequestStatus.Open &&
                                               it.Latitude <= northernMax &&
                                               it.Latitude >= southernMax &&
                                               it.Longitude <= easternMax &&
                                               it.Latitude >= westernMax).Take(amount).ToList();
        }

        /// <summary>
        /// set overdue requests to timeout
        /// </summary>
        public void CloseOverDueRequests()
        {
            var requests = _context.Request.Where(x => x.CreatedOn < DateTime.Today.AddDays(-5) && x.Volunteer != null && x.Status == Request.RequestStatus.Open);
            if(requests != null)
            {
                foreach (Request req in requests)
                {
                    req.Status = Request.RequestStatus.Timeout;
                }
                _context.UpdateRange(requests);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// disposable of dbcontext _context
        /// </summary>
        public void Dispose()
        {

        }
    }
}

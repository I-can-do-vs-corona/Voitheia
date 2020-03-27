using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.BLL
{
    public class MyRequestBll : IMyRequestsBll
    {
        private readonly IMapper _mapper;
        private readonly ACDatabaseContext _context;

        /// <summary>
        /// basic constructur for MyRequests
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        public MyRequestBll(IMapper mapper, ACDatabaseContext context)
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
        public int TakeRequest(int requestId, int userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
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
        /// <param name="userId"></param>
        public void FinishRequest(int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
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
        public void AbortRequest(int requestId, int userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
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
        public Request GetRequest(int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
            {
                return request;
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
        public bool ExistsOnUser(int requestId, int userId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
            {
                return request.Id == requestId && request.Volunteer == userId;
            }
            return false;
        }

        /// <summary>
        /// get all reqeusts of user based on hardcoded user
        /// </summary>
        /// <param name="hardcodedUser"></param>
        /// <returns></returns>
        public List<Request> GetAllPendingFromUser(int hardcodedUser)
        {
            return _context.Request.Where(it => it.Volunteer == hardcodedUser && it.Status == Request.RequestStatus.Pending).ToList();
        }

        /// <summary>
        /// does requests exist bool?
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public bool Exists(int requestId)
        {
            var request = _context.Request.FirstOrDefault(x => x.Id == requestId);
            if(request != null)
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
            if(request != null)
            {
                return true;
            }
            return false;
        }
    }
}

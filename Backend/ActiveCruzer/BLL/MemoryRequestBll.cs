using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.BLL
{
    public class MemoryRequestBll : IRequestBll
    {
        private readonly Dictionary<int,Request> _requests = new Dictionary<int, Request>();
        private int _index = 0;

        /// <inheritdoc />
        public int CreateRequest(Request request)
        {
            Interlocked.Increment(ref _index);
            _requests.Add(_index,request);
            return _index;
        }

        /// <inheritdoc />
        public bool Exists(in int id)
        {
            return _requests.ContainsKey(id);
        }

        /// <inheritdoc />
        public void Delete(in int id)
        {
            _requests.Remove(id);
        }


        /// <inheritdoc />
        public void UpdateStatus(int id, Request.RequestStatus status)
        {
            _requests[id].currentStatus = status;
        }

        /// <inheritdoc />
        public Request GetRequest(in int id)
        {
            return _requests[id];
        }

        /// <inheritdoc />
        public void UpdateAssignee(in int userId, int requestId)
        {
            _requests[requestId].acceptor = userId;
        }


        /// <inheritdoc />
        public List<Request> GetRequestsViaGps(in double latitude, in double longitude, in int amount, in int metersPerimeter)
        {
            return _requests.Values.Take(amount).ToList();
        }


        /// <inheritdoc />
        public void Dispose()
        {

        }

    }
}
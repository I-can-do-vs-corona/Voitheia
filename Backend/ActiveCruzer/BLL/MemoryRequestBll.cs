using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.BLL
{
    public class MemoryRequestBll : IRequestBll, IMyRequestsBll
    {
        private static MemoryRequestBll instance = null;
        private static readonly object padlock = new object();

        MemoryRequestBll()
        {
            for (int i = 0; i < 100; i++)
            {
                
                CreateRequest(new Request
                {
                    City = "Wasserlosen",
                    Street = "Neubessinger Str. 14",
                    Zip = "97535",
                    CreatedOn = DateTime.UtcNow,
                    Description = "Hammer Beschreibung",
                    Email = "hallo@la.com",
                    FirstName = "hallo",
                    LastName = "Ben",
                    Latitude = 1,
                    Longitude = 2,
                    PhoneNumber = "+12351231",
                    RequestType = (RequestType) (i%5+1)
                });
            }
            

            
        }

        public static MemoryRequestBll Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MemoryRequestBll();
                    }

                    return instance;
                }
            }
        }

        private readonly Dictionary<int, Request> _requests = new Dictionary<int, Request>();
        private int _index = 0;

        /// <inheritdoc />
        public int CreateRequest(Request request)
        {
            Interlocked.Increment(ref _index);
            request.Id = _index;
            _requests.Add(_index, request);
            return _index;
        }

        /// <inheritdoc />
        public bool Exists(in int id)
        {
            return _requests.ContainsKey(id);
        }

        public bool IsNotClosed(in int id)
        {
            return _requests[id].Status != Request.RequestStatus.Closed;
        }

        /// <inheritdoc />
        public void Delete(in int id)
        {
            _requests.Remove(id);
        }


        /// <inheritdoc />
        public void UpdateStatus(int id, Request.RequestStatus status)
        {
            _requests[id].Status = status;
        }

        /// <inheritdoc />
        public Request GetRequest(in int id)
        {
            return _requests[id];
        }

        /// <inheritdoc />
        public void UpdateAssignee(in int userId, int requestId)
        {
            _requests[requestId].Volunteer = userId;
        }


        /// <inheritdoc />
        public List<ActiveCruzer.Models.Request> GetRequestsViaGps(in double latitude, in double longitude,
            in int amount,
            in int metersPerimeter)
        {
            return _requests.Values.Take(amount).ToList();
        }


        /// <inheritdoc />
        public void Dispose()
        {
        }


        public int TakeRequest(in int requestId, in int userId)
        {
            var request = _requests[requestId];
            request.Volunteer = userId;
            request.Status = Request.RequestStatus.Pending;
            return requestId;
        }

        public void FinishRequest(in int requestId, in int userId)
        {
            _requests[requestId].Status = Request.RequestStatus.Closed;
        }

        public void AbortRequest(in int requestId, in int userId)
        {
            var request = _requests[requestId];
            request.Volunteer = null;
            request.Status = Request.RequestStatus.Open;
        }

        public Request GetRequest(in int requestId, in int userId)
        {
            return _requests[requestId];
        }

        public bool ExistsOnUser(in int id, in int userId)
        {
            return _requests.ContainsKey(id) && _requests[id]?.Volunteer == userId;
        }

        public List<Request> GetAllFromUser(int hardcodedUser)
        {
            return _requests.Values.Where(it => it.Volunteer == hardcodedUser).ToList();
        }
    }
}
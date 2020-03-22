using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ActiveCruzer.Helper;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.BLL
{
    public class MemoryRequestBll : IRequestBll, IMyRequestsBll
    {
        private static MemoryRequestBll instance = null;
        private static readonly object padlock = new object();

        MemoryRequestBll()
        {
            var addresses = new List<ValidatedAddress>
            {
                new ValidatedAddress
                {
                    City = "Schweinfurt",
                    Street = "Mainberger Straße 36",
                    Zip = "97422",
                    Coordinates = new GeoCoordinate(50.04941467070081,10.247946539857518)
                },
                new ValidatedAddress
                {
                    City = "Schweinfurt",
                    Street = "Niederwerrner Straße 17",
                    Zip = "97421",
                    Coordinates = new GeoCoordinate(50.04772021791362,10.222338545531148)
                },
                new ValidatedAddress
                {
                    City = "Werneck",
                    Street = "Julius-Echter-Straße 15",
                    Zip = "97440",
                    Coordinates = new GeoCoordinate(49.98282398648274,10.097537016741756)
                }
            };

            for (int i = 0; i < 101; i++)
            {
                var addressIndex = i % 3 ;
                CreateRequest(new Request
                {
                    City = addresses[addressIndex].City,
                    Street = addresses[addressIndex].Street,
                    Zip = addresses[addressIndex].Zip,
                    CreatedOn = DateTime.UtcNow,
                    Description = "Hammer Beschreibung",
                    Email = "hallo@la.com",
                    FirstName = "hallo",
                    LastName = "Ben",
                    Latitude = addresses[addressIndex].Coordinates.Latitude,
                    Longitude = addresses[addressIndex].Coordinates.Longitude,
                    PhoneNumber = "+12351231",
                    Status = Request.RequestStatus.Open,
                    RequestType = (RequestType) (i % 5 + 1)
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

        public List<Request> GetRequestsViaGps(GeoCoordinate coordinates, in int amount, in int metersPerimeter)
        {
            var degrees = GeoHelper.MetersToDegree(metersPerimeter);
            var northernMax = coordinates.Latitude + degrees;
            var southernMax = coordinates.Latitude - degrees;
            var westernMax = coordinates.Longitude - degrees;
            var easternMax = coordinates.Longitude + degrees;

            return _requests.Values.Where(it => it.Status == Request.RequestStatus.Open&&
                                                it.Latitude <= northernMax &&
                                                it.Latitude >= southernMax &&
                                                it.Longitude <= easternMax &&
                                                it.Latitude >= westernMax).Take(amount).ToList();
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

        public List<Request> GetAllPendingFromUser(int hardcodedUser)
        {
            return _requests.Values.Where(it => it.Volunteer == hardcodedUser && it.Status == Request.RequestStatus.Pending).ToList();
        }
    }
}
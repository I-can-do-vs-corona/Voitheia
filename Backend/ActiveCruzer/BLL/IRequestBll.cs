using System;
using System.Collections.Generic;
using ActiveCruzer.Models;
using GeoCoordinatePortable;

namespace ActiveCruzer.BLL
{
    public interface IRequestBll : IDisposable
    {
        int CreateRequest(Request request);
        bool Exists(int id);
        void Delete(int id);

        /// <inheritdoc />
        void Dispose();

        void UpdateStatus(int id, Request.RequestStatus status);
        Request GetRequest(int id);
        void UpdateAssignee(int userId, int requestId);
        List<Request> GetRequestsViaGps(GeoCoordinate coordinates, int amount, int metersPerimeter);
    }
}
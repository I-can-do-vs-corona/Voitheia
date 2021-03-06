﻿using System;
using System.Collections.Generic;
using ActiveCruzer.Models;
using GeoCoordinatePortable;

namespace ActiveCruzer.BLL
{
    public interface IRequestBll : IDisposable
    {
        int CreateRequest(Request request, string userId);
        bool Exists(int id);
        void Delete(int id);

        /// <inheritdoc />
        void Dispose();

        void UpdateStatus(int id, Request.RequestStatus status);
        Request GetRequest(int id);
        void UpdateAssignee(string userId, int requestId);
        List<Request> GetRequestsViaGps(GeoCoordinate coordinates, int amount, int metersPerimeter);
    }
}
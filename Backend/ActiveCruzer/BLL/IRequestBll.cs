﻿using System;
using System.Collections.Generic;
using ActiveCruzer.Models;

namespace ActiveCruzer.BLL
{
    public interface IRequestBll : IDisposable
    {
        int CreateRequest(Request request);
        bool Exists(in int id);
        void Delete(in int id);

        /// <inheritdoc />
        void Dispose();

        void UpdateStatus(int id, Request.RequestStatus status);
        Request GetRequest(in int id);
        void UpdateAssignee(in int userId, int requestId);
        List<Request> GetRequestsViaGps(in double latitude, in double longitude, in int amount, in int metersPerimeter);
    }
}
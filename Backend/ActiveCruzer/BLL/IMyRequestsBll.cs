using System;
using System.Collections.Generic;
using ActiveCruzer.Models;

namespace ActiveCruzer.BLL
{
    public interface IMyRequestsBll : IDisposable
    {
        int TakeRequest(in int requestId, in int userId);
        void FinishRequest(in int requestId, in int userId);
        void AbortRequest(in int requestId, in int userId);
        Request GetRequest(in int requestId, in int userId);
        bool ExistsOnUser(in int id, in int userId);
        List<Request> GetAllFromUser(int hardcodedUser);
        bool Exists(in int requestId);
        bool IsNotClosed(in int id);
    }
}
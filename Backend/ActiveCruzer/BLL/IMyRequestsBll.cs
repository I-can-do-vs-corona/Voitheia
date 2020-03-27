using System;
using System.Collections.Generic;
using ActiveCruzer.Models;

namespace ActiveCruzer.BLL
{
    public interface IMyRequestsBll : IDisposable
    {
        int TakeRequest(in int requestId, in int userId);
        void FinishRequest(int requestId);
        void AbortRequest(int requestId, int userId);
        Request GetRequest(int requestId);
        bool ExistsOnUser(int id, int userId);
        List<Request> GetAllPendingFromUser(int hardcodedUser);
        bool Exists(int requestId);
        bool IsNotClosed(int id);
    }
}
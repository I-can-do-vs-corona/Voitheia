using System;
using System.Collections.Generic;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO.Request;

namespace ActiveCruzer.BLL
{
    public interface IMyRequestsBll : IDisposable
    {
        int TakeRequest(int requestId, int userId);
        void FinishRequest(int requestId);
        void AbortRequest(int requestId, int userId);
        RequestDto GetRequest(int requestId, int userId);
        bool ExistsOnUser(int id, int userId);
        List<RequestDto> GetAllPendingFromUser(int hardcodedUser);
        bool Exists(int requestId);
        bool IsNotClosed(int id);
    }
}
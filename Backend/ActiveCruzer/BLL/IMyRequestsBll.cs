using System;
using System.Collections.Generic;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO.Request;

namespace ActiveCruzer.BLL
{
    public interface IMyRequestsBll : IDisposable
    {
        int TakeRequest(int requestId, string userId);
        void FinishRequest(int requestId);
        void AbortRequest(int requestId, string userId);
        RequestDto GetRequest(int requestId, string userId);
        bool ExistsOnUser(int id, string userId);
        List<RequestDto> GetAllPendingFromUser(string hardcodedUser);
        List<RequestComplexDto> GetAllPendingComplex(string userId);
        bool Exists(int requestId);
        bool IsNotClosed(int id);
    }
}
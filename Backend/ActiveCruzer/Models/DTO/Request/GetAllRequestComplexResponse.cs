using System.Collections.Generic;

namespace ActiveCruzer.Models.DTO.Request
{
    public class GetAllMyRequestComplexResponse
    {
        public List<RequestComplexDto> Requests { get; set; }
        public int TotalCount { get; set; }
    }
}
using System.Collections.Generic;

namespace ActiveCruzer.Models.DTO.Request
{
    public class GetAllMyRequestResponse
    {
        public List<RequestDto> Requests { get; set; }
        public int TotalCount { get; set; }
    }
}
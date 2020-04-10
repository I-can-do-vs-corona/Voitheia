using System.Collections.Generic;

namespace ActiveCruzer.Models.DTO.Request
{
    public class GetAllRequestResponse
    {
        public List<MinimalRequestDto> Requests { get; set; }
        public int TotalCount { get; set; }
    }
}
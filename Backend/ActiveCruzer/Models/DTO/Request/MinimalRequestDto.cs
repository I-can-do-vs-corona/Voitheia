using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.DTO.Request
{
    public class MinimalRequestDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Distance to the currently logged in users registered address
        /// </summary>
        public int DistanceToUser { get; set; }
        public RequestType Type { get; set; }
        public RequestStatus Status { get; set; }
    }
}

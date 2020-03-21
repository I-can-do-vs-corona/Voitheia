using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.DTO.Request
{
    /// <summary>
    /// The status of the request
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestStatus
    {
        /// <summary>
        /// The request is still open and not assigned
        /// </summary>
        Open = 1,
        /// <summary>
        /// The request is assigned to a volunteer
        /// </summary>
        Assigned = 2,
        /// <summary>
        /// The request is finished
        /// </summary>
        Closed = 3
    }
}

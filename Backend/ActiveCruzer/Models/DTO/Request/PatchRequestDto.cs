using ActiveCruzer.Models.DTO.MyRequests;

namespace ActiveCruzer.Models.DTO.Request
{
    public class PatchRequestDto
    {
        /// <summary>
        /// The new status
        /// </summary>
        public PatchRequestStatus NewRequestStatus { get; set; }
    }
}
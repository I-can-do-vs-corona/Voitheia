
namespace ActiveCruzer.Models.DTO.Request
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public RequestStatus Status { get; set; }
        /// <summary>
        /// Distance to the currently logged in users registered address
        /// </summary>
        public int DistanceToUser { get; set; }

        public RequestType Type { get; set; }
    }
}

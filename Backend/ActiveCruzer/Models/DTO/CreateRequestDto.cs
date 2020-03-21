using System.ComponentModel.DataAnnotations;

namespace ActiveCruzer.Models.DTO
{
    /// <summary>
    /// The DTO for requests
    /// </summary>
    public class CreateRequestDto
    {
        /// <summary>
        /// The Request type
        /// </summary>
        [Required]
        public RequestType RequestType { get; set; }

        /// <summary>
        /// The last name of the person who needs help
        /// </summary>
        [Required]
        public  string LastName { get; set; }
        /// <summary>
        /// The first name of the person who needs help
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// The email of the person who needs help
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The phone number of the person who needs help
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The street address of the person who needs help
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The zip of the person who needs help
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// The city of the person who needs help
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Comment for the request
        /// </summary>
        public string Comment { get; set; }

    }

    /// <summary>
    /// The type of the request
    /// </summary>
    public enum RequestType
    {
        Shopping,
        Kids,
        Medical,
        Pet,
        Other
    }
}
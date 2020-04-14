using ActiveCruzer.Models.DTO.Request;
using System;

namespace ActiveCruzer.Models.DTO
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
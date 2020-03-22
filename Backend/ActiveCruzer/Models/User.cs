using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.Models
{
    
    public class User : IdentityUser
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IntId { get; set; }
    }
}
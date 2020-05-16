
namespace ActiveCruzer.Models.DTO.Request
{
    public class RequestComplexDto : MinimalRequestDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public MinimalUserDto AssignedUser { get; set; }
        public MinimalUserDto CreaterUser { get; set; }
        public bool Author { get; set; }
        public byte [] ProfilePicture { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ActiveCruzer.Models.DTO
{
    public class CredentialsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
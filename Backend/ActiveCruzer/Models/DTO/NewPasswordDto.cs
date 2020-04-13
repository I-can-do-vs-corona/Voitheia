using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.DTO
{
    public class NewPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string oldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmNewPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.DTO
{
    public class SetNewEmailDto
    {
        [Required]
        [EmailAddress]
        public string oldEmail { get; set; }

        [Required]
        [EmailAddress]
        public string newEmail { get; set; }

        [Required]
        [EmailAddress]
        [Compare("newEmail", ErrorMessage = "The password and confirmation password do not match.")]
        public string newEmailConfirm { get; set; }
    }
}

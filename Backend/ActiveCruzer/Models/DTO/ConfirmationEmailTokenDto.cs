using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.DTO
{
    public class ConfirmationEmailTokenDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string emailToken { get; set; }
    }
}

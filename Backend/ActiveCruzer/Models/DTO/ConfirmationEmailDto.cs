using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.DTO
{
    public class ConfirmationEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ActiveCruzer.Helper;
using BingMapsRESTToolkit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ActiveCruzer.Models.DTO
{
    public class RegisterUserDTO
    {
        private string _street;
        private string _city;
        private string _country;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Street
        {
            get => _street;
            set => _street = value?.ConvertUmlauts();
        }

        public string Zip { get; set; }

        [Required]
        public string City
        {
            get => _city;
            set => _city = value.ConvertUmlauts();
        }

        [Required]
        public string Country
        {
            get => _country;
            set => _country = value.ConvertUmlauts();
        }
        public DateTime TermsAccepted { get; set; }

    }
}
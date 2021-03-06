﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.Models.Geo;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ActiveCruzer.Models.DTO;

namespace ActiveCruzer.Models
{
    /// <summary>
    /// model for requests
    /// </summary>
    [Table("Requests")]
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Volunteer { get; set; }
        public RequestType RequestType { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CreatedBy { get; set; }


        public Request()
        {
        }

        public enum RequestStatus
        {
            Open = 1,
            Pending = 2,
            Closed = 3,
            Timeout = 4
        }
    }
}
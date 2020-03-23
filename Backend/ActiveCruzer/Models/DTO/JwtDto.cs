using System;

namespace ActiveCruzer.Models.DTO
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
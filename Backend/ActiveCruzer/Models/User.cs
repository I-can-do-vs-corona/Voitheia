using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ActiveCruzer.Models
{
    public class User : IdentityUser
    {
        public User(string userName) : base (userName)
        {
            
        }
    }
}

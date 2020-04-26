using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ActiveCruzer.Models
{
    public abstract class ErrorModel
    {
        public abstract int Code { get; }
        public abstract string Errormessage { get; }
    }
}

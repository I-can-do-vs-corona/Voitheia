using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.Error
{
    public class MissingCoordinatesError : ErrorModel
    {
        public override int Code => ErrorCodes.MissingCoordinates;
        public override string Errormessage { get; }
    }
}

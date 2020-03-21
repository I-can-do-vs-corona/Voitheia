using System;
using ActiveCruzer.Models.Geo;

namespace ActiveCruzer.BLL
{
    public class MemoryGeoBll : IGeoCodeBll
    {
        public ValidatedAddress ValidateAddress(GeoQuery query)
        {
            return new ValidatedAddress {Coordinates = new Coordinates {Longitude = 9.709178, Latitude = 50.24512}};
        }
    }
}
using System;
using ActiveCruzer.Models.Geo;

namespace ActiveCruzer.BLL
{
    public class MemoryGeoBll : IGeoCodeBll
    {
        public Coordinates ConvertToCoordinates(GeoQuery query)
        {
            return new Coordinates {Longitude = 9.709178, Latitude = 50.24512};
        }
    }
}
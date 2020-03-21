using ActiveCruzer.Models.Geo;

namespace ActiveCruzer.BLL
{
    public interface IGeoCodeBll
    {
        Coordinates ConvertToCoordinates(GeoQuery query);
    }
}
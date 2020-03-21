using ActiveCruzer.Models.Geo;

namespace ActiveCruzer.BLL
{
    public interface IGeoCodeBll
    {
        ValidatedAddress ValidateAddress(GeoQuery query);
    }
}
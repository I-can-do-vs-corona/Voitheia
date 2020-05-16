using ActiveCruzer.Helper;
using ActiveCruzer.Models.Geo;
using AutoMapper;
using BingMapsRESTToolkit;
using GeoCoordinatePortable;
using Microsoft.Extensions.Configuration;

namespace ActiveCruzer.BLL
{
    public class GeoCodeBll : IGeoCodeBll
    {

        private readonly IMapper _mapper;
        private string _bingKey;

        public GeoCodeBll(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _bingKey = configuration["RingumsBing"];
        }

        public ValidatedAddress ValidateAddress(GeoQuery query)
        {
            var request = new GeocodeRequest
            {
                BingMapsKey = _bingKey,
                Query = query.ToString()
            };

            var result = request.Execute().Result;
            if (result.ResourceSets.Length > 0)
            {
                var location = (Location) result.ResourceSets[0].Resources[0];
                if (location.Address.Locality.IsNullOrWhiteSpace() || location.Address.AddressLine.IsNullOrWhiteSpace())
                {
                    return new ValidatedAddress { ConfidenceLevel = ConfidenceLevel.None };
                }
                else
                {
                    return _mapper.Map<ValidatedAddress>(location);
                }
            }

            return new ValidatedAddress{ConfidenceLevel = ConfidenceLevel.None};
        }

    }

    public class ValidatedAddress
    {
        public ConfidenceLevel ConfidenceLevel { get; set; }
        public string Street { get; set; }
        public GeoCoordinate Coordinates { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
    }

    public enum ConfidenceLevel
    {
        High,
        Medium,
        Low,
        None
        
    }
}
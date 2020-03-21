using ActiveCruzer.Helper;
using ActiveCruzer.Models.Geo;
using AutoMapper;
using BingMapsRESTToolkit;

namespace ActiveCruzer.BLL
{
    public class GeoCodeBll : IGeoCodeBll
    {

        private readonly IMapper _mapper;

        public GeoCodeBll(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ValidatedAddress ValidateAddress(GeoQuery query)
        {
            var request = new GeocodeRequest
            {
                BingMapsKey = "AlYeou04vVPFXJqm9AhT6owjLSWpgA4--oJQamCs4ilOCx-SwNMBzdpQ9esWIhEJ",
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
        public Coordinates Coordinates { get; set; }
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
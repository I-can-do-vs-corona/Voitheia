using ActiveCruzer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ActiveCruzer.Models.DTO.Geo
{
    /// <summary>
    /// Query parameters for the <see cref="GeoController#GetGeoCode"/> endpoint
    /// </summary>
    public class GetGeoCodeQueryParameters
    {
        /// <summary>
        /// The street
        /// </summary>
        [BindRequired]
        public string Street { get; set; }
        /// <summary>
        /// The zip
        /// </summary>
        [BindRequired]
        public string Zip { get; set; }
        /// <summary>
        /// The City
        /// </summary>
        [BindRequired]
        public string City { get; set; }
        /// <summary>
        /// The Country
        /// </summary>
        [BindRequired]
        public string Country { get; set; }
    }
}
namespace ActiveCruzer.Models.DTO.Geo
{
    /// <summary>
    /// The response of the GetGeoCode call. 
    /// </summary>
    public class GetGeoCodeResponse
    {
        /// <summary>
        /// The coordinates of the provided address. Only valid if Result is Match>
        /// </summary>
        public CoordinatesDto Coordinates { get; set; }

        /// <summary>
        /// The result of the GeoCode matching
        /// </summary>
        public GeoCodeMatchResult Result { get; set; }

    }
}
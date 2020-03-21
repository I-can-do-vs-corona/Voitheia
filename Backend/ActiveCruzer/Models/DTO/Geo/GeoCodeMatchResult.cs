using System.Text.Json.Serialization;

namespace ActiveCruzer.Models.DTO.Geo
{
    /// <summary>
    /// The result of the GeoCode request. 
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GeoCodeMatchResult
    {
        /// <summary>
        /// The address was found and could be converted to gps coordinates
        /// </summary>
        Match,
        /// <summary>
        /// The address was not found
        /// </summary>
        NoMatch
    }
}
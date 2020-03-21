using System.Text.Json.Serialization;

namespace ActiveCruzer.Models.DTO.Request
{
    /// <summary>
    /// The type of the request
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestType
    {
        Shopping = 1,
        Childcare = 2,
        Medical = 3,
        Petcare = 4,
        Other = 5
    }
}
using System.Text.Json.Serialization;

namespace ActiveCruzer.Models.DTO.Request
{
    /// <summary>
    /// The type of the request
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestType
    {
        Shopping,
        Childcare,
        Medical,
        Petcare,
        Other
    }
}
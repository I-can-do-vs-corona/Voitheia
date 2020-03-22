using System.Text.Json.Serialization;

namespace ActiveCruzer.Models.DTO.Request
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PatchRequestStatus
    {
        Closed = 1
    }
}

using System.Text.Json.Serialization;

namespace ActiveCruzer.Models.DTO.MyRequests
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PatchRequestStatus
    {
        Closed = 1
    }
}

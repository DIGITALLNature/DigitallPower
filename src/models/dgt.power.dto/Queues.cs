using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class Queues : Assignee
{
    //[JsonPropertyName("queue_pattern")]
    //internal List<string> QueuePattern { get; set; } = new List<string>();

    [JsonPropertyName("queues")]
    [JsonRequired]
    public ICollection<Queue> QueuesToTransport { get; init; } = new List<Queue>();
}

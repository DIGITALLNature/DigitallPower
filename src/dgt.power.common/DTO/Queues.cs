// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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

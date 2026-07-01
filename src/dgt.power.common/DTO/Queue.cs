// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

#pragma warning disable CA1711
#pragma warning disable CS8618

namespace dgt.power.dto;

public sealed class Queue
{
    public enum IncomingEmailDeliveryMethod
    {
        None = 0,
        ServerSideSynchronizationOrEmailRouter = 2,
        ForwardMailbox = 3
    }

    public enum IncomingEmailFilteringMethod
    {
        AllEmailMessages = 0,
        EmailMessagesInResponseToDynamics365Email = 1,
        EmailMessagesFromDynamics365LeadsContactsAndAccounts = 2,
        EmailMessagesFromDynamics365RecordsThatAreEmailEnabled = 3,
        NoEmailMessages = 4
    }

    public enum OutgoingEmailDeliveryMethod
    {
        None = 0,
        ServerSideSynchronizationOrEmailRouter = 2
    }

    public enum QueueViewType
    {
        Public = 0,
        Private = 1
    }

    [JsonPropertyName("queueid")] public required Guid QueueId { get; init; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("viewtype")] public required QueueViewType ViewType { get; set; }

    [JsonPropertyName("incomingemailfiltering")]
    public required IncomingEmailFilteringMethod IncomingEmailFiltering { get; init; }

    [JsonPropertyName("incomingemaildelivery")]
    public IncomingEmailDeliveryMethod IncomingEmailDelivery { get; init; } = IncomingEmailDeliveryMethod.None;

    [JsonPropertyName("outgoingemaildelivery")]
    public OutgoingEmailDeliveryMethod OutgoingEmailDelivery { get; init; } = OutgoingEmailDeliveryMethod.None;

    [JsonPropertyName("description")] public string? Description { get; init; }
}

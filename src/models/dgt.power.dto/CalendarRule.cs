#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class CalendarRule
{
    [JsonPropertyName("calendarruleid")] public required Guid CalendarRuleId { get; init; }

    [JsonPropertyName("name")] public required string Name { get; init; }

    [JsonPropertyName("start_date")] public DateTime? StartDate { get; init; }

    [JsonPropertyName("end_date")] public DateTime? EndDate { get; init; }

    [JsonPropertyName("duration")] public int? Duration { get; init; }

    [JsonPropertyName("Description")] public string? Description { get; init; }

    [JsonPropertyName("Pattern")] public string? Pattern { get; init; }

    [JsonPropertyName("Rank")] public int? Rank { get; init; }

    [JsonPropertyName("SubCode")] public int? SubCode { get; init; }

    [JsonPropertyName("TimeCode")] public int? TimeCode { get; init; }

    [JsonPropertyName("TimeZoneCode")] public int? TimeZoneCode { get; init; }

    [JsonPropertyName("ExtendCode")] public int? ExtendCode { get; init; }

    [JsonPropertyName("Offset")] public int? Offset { get; init; }

    [JsonPropertyName("IsSelected")] public bool? IsSelected { get; init; }

    [JsonPropertyName("IsVaried")] public bool? IsVaried { get; init; }

    [JsonPropertyName("IsSimple")] public bool? IsSimple { get; init; }

    [JsonPropertyName("EffectiveIntervalEnd")]
    public DateTime? EffectiveIntervalEnd { get; init; }

    [JsonPropertyName("EffectiveIntervalStart")]
    public DateTime? EffectiveIntervalStart { get; init; }

    [JsonPropertyName("Effort")] public double? Effort { get; init; }

    [JsonPropertyName("Groupdesignator")] public string? Groupdesignator { get; init; }

    [JsonPropertyName("InnerCalendar")] public Calendar? InnerCalendar { get; init; }
}

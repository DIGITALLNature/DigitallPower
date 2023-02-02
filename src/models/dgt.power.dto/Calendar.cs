using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class Calendar
{
    [JsonPropertyName("CalendarId")] public required Guid CalendarId { get; init; }

    [JsonPropertyName("Name")] public required string Name { get; init; }

    [JsonPropertyName("IsVaryByDay")] public bool IsVaryByDay { get; set; }

    [JsonPropertyName("Type")] public required int Type { get; init; }

    [JsonPropertyName("Description")] public string? Description { get; init; }

    [JsonPropertyName("HolidaySchedule")] public Guid? HolidayScheduleId { get; init; }

    [JsonPropertyName("CalendarRules")] public List<CalendarRule> Rules { get; set; } = new();
}

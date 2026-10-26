// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.duplicaterules.tests.Fakes;

/// <summary>
/// Advances the clock by each timer's due time and fires the timer immediately, so polling loops and
/// deadlines can be tested deterministically.
/// </summary>
public sealed class ManualTimeProvider : TimeProvider
{
    public DateTimeOffset Now { get; private set; } = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public int Delays { get; private set; }

    public Action<int>? OnDelay { get; set; }

    public override DateTimeOffset GetUtcNow() => Now;

    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        ArgumentNullException.ThrowIfNull(callback);
        if (dueTime != Timeout.InfiniteTimeSpan)
        {
            Now += dueTime;
            Delays++;
            OnDelay?.Invoke(Delays);
            callback(state);
        }

        return new CompletedTimer();
    }

    private sealed class CompletedTimer : ITimer
    {
        public bool Change(TimeSpan dueTime, TimeSpan period) => false;

        public void Dispose()
        {
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

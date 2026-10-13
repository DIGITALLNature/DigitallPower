// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.tests;

public sealed class ImmediateTimeProvider : TimeProvider
{
    public static ImmediateTimeProvider Instance { get; } = new();

    private ImmediateTimeProvider()
    {
    }

    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        var timer = new ImmediateTimer(callback, state);
        timer.Change(dueTime, period);
        return timer;
    }

    private sealed class ImmediateTimer(TimerCallback callback, object? state) : ITimer
    {
        private bool _disposed;

        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            if (_disposed)
            {
                return false;
            }

            if (dueTime != Timeout.InfiniteTimeSpan)
            {
                callback(state);
            }

            return true;
        }

        public void Dispose() => _disposed = true;

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }
    }
}

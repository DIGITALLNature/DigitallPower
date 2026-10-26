// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public static class UnmappedOwnerHandlingOption
{
    public const string Fail = "fail";
    private const string Skip = "skip";

    public static bool TryParse(string? value, out UnmappedOwnerHandling handling)
    {
        switch (value)
        {
            case Fail:
                handling = UnmappedOwnerHandling.Fail;
                return true;
            case Skip:
                handling = UnmappedOwnerHandling.Skip;
                return true;
            default:
                handling = UnmappedOwnerHandling.Fail;
                return false;
        }
    }

    public static UnmappedOwnerHandling Parse(string? value) =>
        TryParse(value, out var handling)
            ? handling
            : throw new ArgumentException("--unmapped-owners must be fail or skip.", nameof(value));
}

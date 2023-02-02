using FluentAssertions;
using FluentAssertions.Primitives;

namespace dgt.power.tests.Extensions;

public static class AssertionExtensions
{
    public static AndConstraint<BooleanAssertions> Succeed(this BooleanAssertions assertion) => assertion.BeTrue();
    public static AndConstraint<BooleanAssertions> Fail(this BooleanAssertions assertion) => assertion.BeFalse();
}

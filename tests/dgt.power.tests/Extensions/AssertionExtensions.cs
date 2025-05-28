// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using AwesomeAssertions;
using AwesomeAssertions.Primitives;

namespace dgt.power.tests.Extensions;

public static class AssertionExtensions
{
    public static AndConstraint<BooleanAssertions> Succeed(this BooleanAssertions assertion) => assertion.BeTrue();
    public static AndConstraint<BooleanAssertions> Fail(this BooleanAssertions assertion) => assertion.BeFalse();
}

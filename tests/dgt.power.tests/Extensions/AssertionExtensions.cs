// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace dgt.power.tests.Extensions;

public static class AssertionExtensions
{
    public static async Task Succeed(this bool result) => await Assert.That(result).IsTrue();
    public static async Task Fail(this bool result) => await Assert.That(result).IsFalse();
}

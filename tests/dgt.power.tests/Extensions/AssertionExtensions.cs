// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL


namespace dgt.power.tests.Extensions;

public static class AssertionExtensions
{
    public static async Task Succeed(this bool result) => await Assert.That(result).IsTrue();
    public static async Task Fail(this bool result) => await Assert.That(result).IsFalse();
}

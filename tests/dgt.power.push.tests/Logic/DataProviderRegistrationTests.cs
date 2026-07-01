// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.push.Logic;
using dgt.power.push.Model;

namespace dgt.power.push.tests.Logic;

public class DataProviderRegistrationTests
{
    [Test]
    [Arguments(0, "Retrieve")]
    [Arguments(1, "RetrieveMultiple")]
    [Arguments(2, "Create")]
    [Arguments(3, "Update")]
    [Arguments(4, "Delete")]
    public async Task MapDataProviderEventToMessage_ReturnsCorrectMessage(int eventValue, string expectedMessage)
    {
        // Act
        var result = AssemblyModelBuilder.MapDataProviderEventToMessage(eventValue);

        // Assert
        await Assert.That(result).IsEqualTo(expectedMessage);
    }

    [Test]
    public async Task MapDataProviderEventToMessage_ThrowsForUnknownEvent()
    {
        // Act & Assert
        await Assert.That(() => AssemblyModelBuilder.MapDataProviderEventToMessage(99))
            .ThrowsExactly<AssemblyException>();
    }
}

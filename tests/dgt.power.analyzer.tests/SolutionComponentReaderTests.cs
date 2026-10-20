// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.analyzer.tests;

public class SolutionComponentReaderTests
{
    [Test]
    [Arguments("components")]
    [Arguments("layers")]
    [Arguments("solutionService")]
    [Arguments("uniqueName")]
    public async Task NullArgument_ShouldThrowSynchronously(string argument)
    {
        var reader = new SolutionComponentReader();
        Action invoke = argument switch
        {
            "components" => () => { _ = reader.GetSolutionComponentsAsync(null!, Guid.Empty, CancellationToken.None); }
            ,
            "layers" => () => { _ = reader.GetSolutionLayersAsync(null!, new SolutionComponent(), CancellationToken.None); }
            ,
            "solutionService" => () => { _ = SolutionComponentReader.GetSolutionAsync(null!, "solution", CancellationToken.None); }
            ,
            "uniqueName" => () => { _ = SolutionComponentReader.GetSolutionAsync(Mock.Of<IOrganizationServiceAsync2>().Object, null!, CancellationToken.None); }
            ,
            _ => throw new ArgumentOutOfRangeException(nameof(argument))
        };

        await Assert.That(invoke).Throws<ArgumentNullException>();
    }
}

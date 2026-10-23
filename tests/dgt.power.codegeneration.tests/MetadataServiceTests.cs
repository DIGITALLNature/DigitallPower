// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Caching;
using dgt.power.codegeneration.Services;
using dgt.power.common;
using Spectre.Console.Testing;

namespace dgt.power.codegeneration.tests;

public class MetadataServiceTests
{
    [Test]
    [Arguments("entity")]
    [Arguments("configSolutions")]
    [Arguments("config")]
    [Arguments("requests")]
    [Arguments("sdkMessages")]
    [Arguments("optionSets")]
    public async Task NullArgument_ShouldThrowSynchronously(string argument)
    {
        using var cache = new MemoryCache(nameof(NullArgument_ShouldThrowSynchronously));
        using var console = new TestConsole();
        var connection = Mock.Of<IDataverseConnection>();
        connection.GetServiceAsync(Any<Guid>()).Throws(new NotSupportedException(
            "This metadata test double does not support caller services."));
        connection.GetServiceAsync(Guid.Empty).Throws(new ArgumentException(
            "A Dataverse systemuser ID is required."));
        var service = new MetadataService(connection.Object, cache, console);
        Action invoke = argument switch
        {
            "entity" => () => { _ = service.RetrieveEntityMetadataAsync(null!); }
            ,
            "configSolutions" => () => { _ = service.RetrieveFormsDetailsFromSolutionsAsync("account", null!, null); }
            ,
            "config" => () => { _ = service.PopulateEntitiesAndSolutionsAsync(null!); }
            ,
            "requests" => () => { _ = service.RetrieveRequestsAsync(null!); }
            ,
            "sdkMessages" => () => { _ = service.RetrieveSdkMessageNamesAsync(null!); }
            ,
            "optionSets" => () => { _ = service.RetrieveOptionSetsAsync(null!); }
            ,
            _ => throw new ArgumentOutOfRangeException(nameof(argument))
        };

        await Assert.That(invoke).Throws<ArgumentNullException>();
    }
}

// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.integration.tests;

public class PoC : IntegrationTests
{
    [SkippableFact]
    public void DurchStich()
    {
        Skip.IfNot(IntegrationTestsEnabled);

        //Assert.True(SetTestEnv());

        var entryPoint = typeof(Program).Assembly.EntryPoint!;

        var args = "profile create \"INTEGRATIONTEST\"".Split(' ').ToList();
        args.Add($"{Connectionstring}");
        var exitCode = entryPoint.Invoke(null, new object[] { args.ToArray() });
        Assert.Equal(0,exitCode);
    }
    
}

// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Core;

namespace dgt.power.common.tests.Logic;

internal sealed class TestTokenCredential : TokenCredential
{
    internal List<string> Scopes { get; } = [];
    internal Exception? Failure { get; init; }

    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
        throw new NotSupportedException("The connector must acquire tokens asynchronously.");

    public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext,
        CancellationToken cancellationToken)
    {
        Scopes.AddRange(requestContext.Scopes);
        return Failure is { } failure
            ? ValueTask.FromException<AccessToken>(failure)
            : ValueTask.FromResult(new AccessToken("test-token-not-for-display", DateTimeOffset.MaxValue));
    }
}

// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console.Cli;

namespace dgt.power.common;

public sealed class TypeResolver : ITypeResolver, IDisposable
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider) => _provider = provider ?? throw new ArgumentNullException(nameof(provider));

    public void Dispose()
    {
        if (_provider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public object? Resolve(Type? type)
    {
        if (type == null)
        {
            return null;
        }

        return _provider.GetService(type);
    }
}

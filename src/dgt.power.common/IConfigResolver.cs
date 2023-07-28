// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common;

public interface IConfigResolver
{
    bool TryGetConfigFile<TC>(string fileDir, string file, out TC config) where TC : class, new();
    bool TryGetConfigFile<TC>(string file, out TC config) where TC : class,new();
    bool TryConfigFromFile<T>(string file, out T obj) where T : new();
}

// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common;

public interface IConfigResolver
{
    bool GetConfigFile<TC>(string fileDir, string file, out TC config) where TC : new();
    bool GetConfigFile<TC>(string file, out TC config) where TC : new();
    bool ConfigFromFile<T>(string file, out T obj) where T : new();
}

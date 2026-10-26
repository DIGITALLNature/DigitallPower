// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common;

public interface IConfigResolver
{
    bool TryGetConfigFile<TC>(string file, out TC config) where TC : class,new();
    bool TryConfigFromFile<T>(string file, out T obj) where T : new();
}

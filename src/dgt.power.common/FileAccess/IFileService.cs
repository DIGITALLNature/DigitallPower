// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.FileAccess;

public interface IFileService
{
    string ExportJsonFile<TObject>(string fileDirectory, string fileName, TObject jsonObject);
    // ReSharper disable once UnusedMemberInSuper.Global -- retained as part of the shared text file-service contract.
    string ExportFile(string fileDirectory, string fileName, string content);
    // ReSharper disable once UnusedMemberInSuper.Global -- retained as part of the shared binary file-service contract.
    string ExportFile(string fileDirectory, string fileName, byte[] content);
}

// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.FileAccess;

public interface IFileService
{
    string ExportJsonFile<TObject>(string fileDirectory, string fileName, TObject jsonObject);
    string ExportFile(string fileDirectory, string fileName, string content);
    string ExportFile(string fileDirectory, string fileName, byte[] content);
}

// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Reflection;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators.Worker;

public abstract class TypescriptGeneratorWorker
{

    private const string NOT_NULL = "!= null";
    /// <summary>
    ///     Creates a TypeScript file with the specified content and name in the target directory.
    /// </summary>
    /// <param name="content">The content to be written into the file.</param>
    /// <param name="name">The name of the file to be created.</param>
    /// <param name="args">The code generation arguments, including target directory and folder configuration.</param>
    /// <param name="outputPath">Additional path in the output ts script folder</param>
    public void CreateFile(string content, string name, CodeGenerationVerb args, string[]? outputPath = null)
    {
        // Ensure that the template and args are not null
        Debug.Assert(content != null, $"{nameof(content)} {NOT_NULL}");
        Debug.Assert(args != null, $"{nameof(args)} {NOT_NULL}");
        Debug.Assert(name != null, $"{nameof(name)} + { NOT_NULL}");
        // Combine the target directory, folder, and file name to create the full path
        var path = Path.Combine([
            args.TargetDirectory,
            args.Folder,
            Folders.Typescript,
            ..(outputPath ?? ([])),
            $"{name}.d.ts"
        ]);

        // Create directly if needed
        CreateDirectoryWhenNeeded(Path.GetDirectoryName(path));
        // Create a text file at the specified path
        using var file = File.CreateText(path);
        // Print a message indicating the file creation
        AnsiConsole.MarkupLine($"Creating File: [bold green] {path} [/]");
        // Write the generated content to the file
        file.Write(content);
    }

    public void CopyTemplateFileContent(CodeGenerationVerb args, string templateFileName, string extension)
    {
        // Ensure that the template and args are not null
        Debug.Assert(templateFileName != null, $"{nameof(templateFileName)} {NOT_NULL}");
        Debug.Assert(args != null, $"{nameof(args)} {NOT_NULL}");
        Debug.Assert(extension != null, $"{nameof(extension)} {NOT_NULL}");

        var reader = new StreamReader(Assembly.GetCallingAssembly()
            .GetManifestResourceStream($"dgt.power.codegeneration.Templates.tsl.{templateFileName}.{extension}")!);

        var content = reader.ReadToEnd();

        CreateFile(content, templateFileName, args);
    }

    /// <summary>
    /// Prepares the directory for code generation.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    public void PrepareDirectory(CodeGenerationVerb args)
    {
        // Ensure that the arguments are not null
        Debug.Assert(args != null, $"{nameof(args)} {NOT_NULL}");

        // Create the main folder if it doesn't exist
        var mainFolderPath = Path.Combine(args.TargetDirectory, args.Folder);
        CreateDirectoryWhenNeeded(mainFolderPath);

        // Create the Typescript folder if it doesn't exist
        var typescriptFolderPath = Path.Combine(mainFolderPath, Folders.Typescript);
        if (!CreateDirectoryWhenNeeded(typescriptFolderPath)) {
            // Delete all files and folders in the directory file
            var directory = new DirectoryInfo(typescriptFolderPath);
            // Delete files in ts folder
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
            // Delete all subfolders
            foreach (var dir in directory.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }

    /// <summary>
    /// Auxiliary function given a path creates missed directory folders and returns true, false when folder already exists
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static bool CreateDirectoryWhenNeeded(string? directoryPath)
    {
        Debug.Assert(directoryPath != null, $"{nameof(directoryPath)} {NOT_NULL}");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            return true;
        }
        return false;
    }
}

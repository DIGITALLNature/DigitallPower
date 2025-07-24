// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators;

public abstract class TypescriptGeneratorWorker
{
    /// <summary>
    ///     Creates a TypeScript file with the specified content and name in the target directory.
    /// </summary>
    /// <param name="content">The content to be written into the file.</param>
    /// <param name="name">The name of the file to be created.</param>
    /// <param name="args">The code generation arguments, including target directory and folder configuration.</param>
    public void CreateFile(string content, string name, CodeGenerationVerb args)
    {
        // Ensure that the template and args are not null
        Debug.Assert(content != null, nameof(content) + " != null");
        Debug.Assert(args != null, nameof(args) + " != null");

        // Combine the target directory, folder, and file name to create the full path
        var path = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript, $"{name}.ts");
        // Create a text file at the specified path
        using var file = File.CreateText(path);
        // Print a message indicating the file creation
        AnsiConsole.MarkupLine($"Creating File: [bold green] {path} [/]");
        // Write the generated content to the file
        file.Write(content);
    }

    /// <summary>
    ///     Prepares the directory for code generation.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    public void PrepareDirectory(CodeGenerationVerb args)
    {
        // Ensure that the arguments are not null
        Debug.Assert(args != null, nameof(args) + " != null");

        // Create the main folder if it doesn't exist
        var mainFolderPath = Path.Combine(args.TargetDirectory, args.Folder);
        if (!Directory.Exists(mainFolderPath))
        {
            Directory.CreateDirectory(mainFolderPath);
        }

        // Create the Typescript folder if it doesn't exist
        var typescriptFolderPath = Path.Combine(mainFolderPath, Folders.Typescript);
        if (!Directory.Exists(typescriptFolderPath))
        {
            Directory.CreateDirectory(typescriptFolderPath);
        }
        else
        {
            // Delete all .ts files in the Typescript folder
            var typescriptFiles = Directory.GetFiles(typescriptFolderPath, "*.ts");
            foreach (var file in typescriptFiles)
            {
                File.Delete(file);
            }
        }
    }
}

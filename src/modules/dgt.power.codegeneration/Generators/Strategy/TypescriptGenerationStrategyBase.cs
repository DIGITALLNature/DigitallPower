// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators.Strategy;

public abstract class TypescriptGenerationStrategyBase(IAnsiConsole console)
{
    protected IAnsiConsole Console { get; } = console;

    private const string NotNull = "!= null";
    /// <summary>
    ///     Creates a TypeScript file with the specified content and name in the target directory.
    /// </summary>
    /// <param name="content">The content to be written into the file.</param>
    /// <param name="name">The name of the file to be created.</param>
    /// <param name="args">The code generation arguments, including target directory and folder configuration.</param>
    /// <param name="extension">Extension of the file</param>
    /// <param name="outputPath">Additional path in the output ts script folder</param>
    protected void CreateFile(string content, string name, CodeGenerationVerb args, string extension, string[]? outputPath = null)
    {
        // Ensure that the template and args are not null
        Debug.Assert(content != null, $"{nameof(content)} {NotNull}");
        Debug.Assert(args != null, $"{nameof(args)} {NotNull}");
        Debug.Assert(name != null, $"{nameof(name)} {NotNull}");
        // Combine the target directory, folder, and file name to create the full path
        var path = Path.Combine([
            args.TargetDirectory,
            args.Folder,
            Folders.Typescript,
            ..outputPath ?? [],
            $"{name}.{extension}"
        ]);

        // Create directly if needed
        CreateDirectoryWhenNeeded(Path.GetDirectoryName(path));
        // Create a text file at the specified path
        using var file = File.CreateText(path);
        // Print a message indicating the file creation
        Console.MarkupLine($"Creating File: [bold green] {path} [/]");
        // Write the generated content to the file
        file.Write(content);
    }

    protected void CopyTemplateFileContent(CodeGenerationVerb args, string templateFileName, string extension)
    {
        // Ensure that the template and args are not null
        Debug.Assert(templateFileName != null, $"{nameof(templateFileName)} {NotNull}");
        Debug.Assert(args != null, $"{nameof(args)} {NotNull}");
        Debug.Assert(extension != null, $"{nameof(extension)} {NotNull}");

        Console.MarkupLine($"Reading File: [bold green] {templateFileName}.{extension} [/]");

        using var reader = new StreamReader(typeof(TypescriptGenerationStrategyBase).Assembly
            .GetManifestResourceStream($"dgt.power.codegeneration.Templates.tsl.{templateFileName}.{extension}")!);

        var content = reader.ReadToEnd();

        CreateFile(content, templateFileName, args, extension);
    }

    /// <summary>
    /// Prepares the directory for code generation.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    protected static void PrepareDirectory(CodeGenerationVerb args)
    {
        // Ensure that the arguments are not null
        Debug.Assert(args != null, $"{nameof(args)} {NotNull}");

        // Create the main folder if it doesn't exist
        var mainFolderPath = Path.Combine(args.TargetDirectory, args.Folder);
        CreateDirectoryWhenNeeded(mainFolderPath);

        // Create the TypeScript folder if it doesn't exist
        var typescriptFolderPath = Path.Combine(mainFolderPath, Folders.Typescript);
        if (CreateDirectoryWhenNeeded(typescriptFolderPath))
        {
            return;
        }

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

    /// <summary>
    /// Auxiliary function given a path creates missed directory folders and returns true, false when folder already exists
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    private static bool CreateDirectoryWhenNeeded(string? directoryPath)
    {
        Debug.Assert(directoryPath != null, $"{nameof(directoryPath)} {NotNull}");
        if (Directory.Exists(directoryPath))
        {
            return false;
        }

        Directory.CreateDirectory(directoryPath);
        return true;
    }
}

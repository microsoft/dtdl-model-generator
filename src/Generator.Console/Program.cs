// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;



class Program
{
    static async Task<int> Main(string[] args)
    {
        var jsonModelsDirectoryArgument = new Argument<string>("JsonModelsDirectory", "The directory the DTDL json models are located at");
        var outputDirectoryArgument = new Argument<string>("OutputDirectory", "The directory to write the generated C# classes to");
        var namespaceOption = new Option<string?>(
            name: "--Namespace",
            description: "The namespace to apply to the generated classes.");

        var copyrightHeaderOption = new Option<string?>(
            name: "--CopyrightHeader",
            description: "The copyright header to include in the generated classes.");

        var rootCommand = new RootCommand("Tool to parse DTDL json files and to generate c# POCO classes");
        rootCommand.AddArgument(jsonModelsDirectoryArgument);
        rootCommand.AddArgument(outputDirectoryArgument);
        rootCommand.AddOption(namespaceOption);
        rootCommand.AddOption(copyrightHeaderOption);

        rootCommand.SetHandler(async (jsonModelsDirectoryV, outputDirectoryV, namespaceV, copyrightHeaderV) =>
        {
            await GenerateClasses(jsonModelsDirectoryV, outputDirectoryV, namespaceV, copyrightHeaderV);
        },
            jsonModelsDirectoryArgument, outputDirectoryArgument, namespaceOption, copyrightHeaderOption);

        return await rootCommand.InvokeAsync(args);
    }

    internal static async Task GenerateClasses(string jsonModelsDirectoryV, string outputDirectoryV, string? namespaceV, string? copyrightHeaderV)
    {
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outputDirectoryV,
            JsonModelsDirectory = jsonModelsDirectoryV,
            Namespace = namespaceV,
            CopyrightHeader = copyrightHeaderV
        };
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.

        var generator = new ModelGenerator(options);
        if (Directory.Exists(options.OutputDirectory))
        {
            Directory.Delete(options.OutputDirectory, true);
        }
        await generator.GenerateClassesAsync().ConfigureAwait(false);
    }
}




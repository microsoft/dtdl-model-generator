// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class ModelGeneratorUnitTests
{
    // Because this clears the directory prior to generation, this causes the other unit tests to hang.
    // As a result this test can only be run manually on it's own until we can change how we clear out
    // the generated directory.
    [Ignore] 
    [TestMethod]
    public async Task CanGenerateClasses()
    {
        var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = "..\\..\\..\\..\\Generator.Tests.Generated",
            IncludeTemplateProject = true,
            Namespace = "Generator.Tests.Generated",
            JsonModelsDirectory = jsonDir
        };

        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync();
        Process.Start("dotnet.exe", "restore \"..\\..\\..\\..\\Generator.Tests.Generated\"");
    }
}
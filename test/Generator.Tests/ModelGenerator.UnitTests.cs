// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class ModelGeneratorUnitTests
{
    [TestMethod]
    public void CanConstructGenerator()
    {
        var options = new ModelGeneratorOptions { Namespace = "Generator.Tests.Generated" };
        var generator = new ModelGenerator(options);
        Assert.IsNotNull(generator);
    }

    [TestMethod]
    public async Task CanGenerateClasses()
    {
        var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
        var outDir = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\Generator.Tests.Generated");
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generator.Tests.Generated",
            JsonModelsDirectory = jsonDir
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task GenerationCreatesOutputDirectoryIfNotExists()
    {
        var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
        var outDir = Path.Combine(Directory.GetCurrentDirectory(), "Generated.NoProject");
        if (Directory.Exists(outDir))
        {
            Directory.Delete(outDir, true);
        }

        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generator.Tests.Generated.NoProject",
            JsonModelsDirectory = jsonDir
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task GenerationCleansOutputDirectoryOfNonGeneratedFiles()
    {
        var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
        var outDir = Path.Combine(Directory.GetCurrentDirectory(), "Generated.NoProject");
        File.Create(Path.Combine(outDir, "TestFile.cs")).Dispose();
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generator.Tests.Generated.NoProject",
            JsonModelsDirectory = jsonDir
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
        Assert.IsFalse(File.Exists(Path.Combine(outDir, "TestFile.cs")));
    }

    private async Task RunGeneratorAndAssertFilesGeneratedAsync(ModelGeneratorOptions options)
    {
        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(500);
        AssertHelper.AssertFilesGenerated(options.JsonModelsDirectory, options.OutputDirectory);
    }
}
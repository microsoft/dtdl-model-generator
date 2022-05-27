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
    public async Task CanGenerateWithTemplateProject()
    {
        var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
        var outDir = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\Generator.Tests.Generated");
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            IncludeTemplateProject = true,
            Namespace = "Generator.Tests.Generated",
            JsonModelsDirectory = jsonDir
        };

        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(1000);
        AssertFilesGenerated(jsonDir, options.OutputDirectory);
    }

    [TestMethod]
    public async Task CanGenerateWithoutTemplateProject()
    {
        var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
        var outDir = Path.Combine(Directory.GetCurrentDirectory(), "Generated.NoProject");
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            IncludeTemplateProject = false,
            Namespace = "Generator.Tests.Generated.NoProject",
            JsonModelsDirectory = jsonDir
        };

        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(1000);
        AssertFilesGenerated(jsonDir, options.OutputDirectory);
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
            IncludeTemplateProject = false,
            Namespace = "Generator.Tests.Generated.NoProject",
            JsonModelsDirectory = jsonDir
        };

        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(1000);
        AssertFilesGenerated(jsonDir, options.OutputDirectory);
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
            IncludeTemplateProject = false,
            Namespace = "Generator.Tests.Generated.NoProject",
            JsonModelsDirectory = jsonDir
        };

        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(1000);
        AssertFilesGenerated(jsonDir, options.OutputDirectory);
        Assert.IsFalse(File.Exists(Path.Combine(outDir, "TestFile.cs")));
    }

    /// <summary>
    /// This validates that at least as many models were found were generated, even though generation does
    /// produce individual cs files for certain properties that are in the json models.
    /// </summary>
    private void AssertFilesGenerated(string jsonDir, string outputDir)
    {
        var testJsonModels = Directory.GetFiles(jsonDir, "*.json");
        var generatedFiles = Directory.GetFiles(outputDir, "*.cs", SearchOption.AllDirectories);
        foreach (var testJsonModel in testJsonModels)
        {
            var expectedFile = Path.GetFileNameWithoutExtension(testJsonModel);
            var matchingFile = generatedFiles.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == expectedFile);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(matchingFile), $"Expected to find generated file for {expectedFile}");
        }
    }
}
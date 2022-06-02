// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class ModelGeneratorUnitTests
{
    private string currentDir = string.Empty;

    private string[] customFiles = { };

    [TestInitialize]
    public void Initialize()
    {
        currentDir = Directory.GetCurrentDirectory();
        customFiles = Directory.GetFiles(PathHelper.GetCombinedFullPath(currentDir, "src/Generator.TemplateProject/Custom", 5), "*.cs", SearchOption.TopDirectoryOnly);
    }

    [TestMethod]
    public void CanConstructGenerator()
    {
        var options = new ModelGeneratorOptions();
        var generator = new ModelGenerator(options);
        Assert.IsNotNull(generator);
    }

    [TestMethod]
    public async Task CanGenerateClasses()
    {
        var jsonDir = Path.Combine(currentDir, "TestDtdlModels");
        var outDir = PathHelper.GetCombinedFullPath(currentDir, "Generated.WithProject");
        if (Directory.Exists(outDir))
        {
            Directory.Delete(outDir, true);
        }

        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generator.WithProject",
            JsonModelsDirectory = jsonDir
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
        AssertCustomFilesCopied(options.OutputDirectory);
    }

    [TestMethod]
    public async Task GenerationCreatesOutputDirectoryIfNotExists()
    {
        var jsonDir = Path.Combine(currentDir, "TestDtdlModels");
        var outDir = Path.Combine(currentDir, "Generated.NoProject");
        if (Directory.Exists(outDir))
        {
            Directory.Delete(outDir, true);
        }

        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generated.NoProject",
            JsonModelsDirectory = jsonDir
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
        AssertCustomFilesCopied(options.OutputDirectory);
    }

    [TestMethod]
    public async Task GenerationCleansOutputDirectoryOfNonGeneratedFiles()
    {
        var jsonDir = Path.Combine(currentDir, "TestDtdlModels");
        var outDir = Path.Combine(currentDir, "Generated.CleansOutputDirectory");
        if (Directory.Exists(outDir))
        {
            Directory.Delete(outDir, true);
        }

        Directory.CreateDirectory(outDir);
        File.Create(Path.Combine(outDir, "TestFile.cs")).Dispose();
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generated.CleansOutputDirectory",
            JsonModelsDirectory = jsonDir
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
        Assert.IsFalse(File.Exists(Path.Combine(outDir, "TestFile.cs")));
        AssertCustomFilesCopied(options.OutputDirectory);
    }

    [TestMethod]
    public async Task GenerationAddsCopyrightHeaderIfIncluded()
    {
        var jsonDir = Path.Combine(currentDir, "TestDtdlModels");
        var outDir = Path.Combine(currentDir, "Generated.WithCopyrightHeader");
        if (Directory.Exists(outDir))
        {
            Directory.Delete(outDir, true);
        }

        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generated.WithCopyrightHeader",
            JsonModelsDirectory = jsonDir,
            CopyrightHeader = "// Copyright (c) Microsoft Corporation.\n// Licensed under the MIT License."
        };

        await RunGeneratorAndAssertFilesGeneratedAsync(options).ConfigureAwait(false);
        AssertCustomFilesCopied(options.OutputDirectory);
    }

    private void AssertCustomFilesCopied(string outDir)
    {
        foreach (var customFile in customFiles)
        {
            var fileName = Path.GetFileName(customFile);
            Assert.IsTrue(File.Exists(Path.Combine(outDir, fileName)), $"{fileName} was not copied");
        }
    }

    private async Task RunGeneratorAndAssertFilesGeneratedAsync(ModelGeneratorOptions options)
    {
        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(500);
        AssertHelper.AssertFilesGenerated(options.JsonModelsDirectory, options.OutputDirectory);
    }
}
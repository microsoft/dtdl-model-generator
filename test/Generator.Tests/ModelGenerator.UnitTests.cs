// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class ModelGeneratorUnitTests
{
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
    }
}
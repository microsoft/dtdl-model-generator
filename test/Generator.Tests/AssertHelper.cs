// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

public class AssertHelper
{
    public static void AssertFilesGenerated(string jsonDir, string outDir)
    {
        var jsonFiles = Directory.GetFiles(jsonDir, "*.json", SearchOption.AllDirectories);
        var outFiles = Directory.GetFiles(outDir, "*.cs", SearchOption.AllDirectories);
        var outFileNames = outFiles.Select(Path.GetFileName).ToList();
        foreach (var jsonFile in jsonFiles)
        {
            var jsonFileName = Path.GetFileName(jsonFile);
            var outFileName = jsonFileName.Replace(".json", ".cs");
            Assert.IsTrue(outFileNames.Contains(outFileName), $"{outFileName} was not generated");
        }
    }

    // /// <summary>
    // /// This validates that at least as many models were found were generated, even though generation does
    // /// produce individual cs files for certain properties that are in the json models.
    // /// </summary>
    // private void AssertFilesGenerated(string jsonDir, string outputDir)
    // {
    //     var testJsonModels = Directory.GetFiles(jsonDir, "*.json");
    //     var generatedFiles = Directory.GetFiles(outputDir, "*.cs", SearchOption.AllDirectories);
    //     foreach (var testJsonModel in testJsonModels)
    //     {
    //         var expectedFile = Path.GetFileNameWithoutExtension(testJsonModel);
    //         var matchingFile = generatedFiles.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == expectedFile);
    //         Assert.IsTrue(!string.IsNullOrWhiteSpace(matchingFile), $"Expected to find generated file for {expectedFile}");
    //     }
    // }
}
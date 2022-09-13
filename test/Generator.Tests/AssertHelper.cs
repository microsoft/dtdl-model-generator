// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

internal static class AssertHelper
{
    internal static void AssertFilesGenerated(string jsonDir, string outDir)
    {
        var jsonFiles = Directory.GetFiles(jsonDir, "*.json", SearchOption.AllDirectories);
        var outFiles = Directory.GetFiles(outDir, "*.cs", SearchOption.AllDirectories);
        var outFileNames = outFiles.Where(f => !f.Contains("obj")).Select(Path.GetFileName).ToList();
        foreach (var jsonFile in jsonFiles)
        {
            var jsonFileName = Path.GetFileName(jsonFile);
            var outFileName = jsonFileName.Replace(".json", ".cs");
            Assert.IsTrue(outFileNames.Contains(outFileName), $"{outFileName} was not generated");
        }

        // Includes custom files
        Assert.AreEqual(45, outFileNames.Count, "Expected 45 files to be generated");
    }
}
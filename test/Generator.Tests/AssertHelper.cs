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
}
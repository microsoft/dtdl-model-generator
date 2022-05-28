// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

/// <summary>
/// A class used to generate C# POCO models from DTDL json files.
/// </summary>
public class ModelGenerator
{
    private ModelGeneratorOptions options { get; set; }

    private IList<string> generatedFiles = new List<string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelGenerator"/> class.
    /// </summary>
    /// <param name="options">The options used to configure behavior of the Generator.</param>
    public ModelGenerator(ModelGeneratorOptions options)
    {
        this.options = options;
    }

    /// <summary>
    /// Asynchronously generates C# POCO clases based on DTDL json files
    /// in the directory provided by the generator options and then writes them to
    /// the provided directory set in the generator options.
    /// </summary>
    public async Task GenerateClassesAsync()
    {
        if (!Directory.Exists(options.OutputDirectory))
        {
            Directory.CreateDirectory(options.OutputDirectory);
        }

        var files = GetJsonModels();
        var parser = new ModelParser();
        var parsed = await parser.ParseAsync(files).ConfigureAwait(false);
        var models = parsed.Where(i => i.Value.EntityKind == DTEntityKind.Interface).ToDictionary(p => p.Key, p => (DTInterfaceInfo)p.Value).Values;
        await CopyCustomModelsAsync().ConfigureAwait(false);
        GenerateModels(models);
        CleanupOutputDirectory();
    }

    private void CleanupOutputDirectory()
    {
        var files = Directory.GetFiles(options.OutputDirectory, "*.cs", SearchOption.AllDirectories);
        var noBinOrObjFolders = files.Where(f => !f.Contains($"{options.Namespace}\\bin") && !f.Contains($"{options.Namespace}\\obj"));
        foreach (var file in noBinOrObjFolders)
        {
            var fileName = Path.GetFileName(file);
            if (!generatedFiles.Contains(fileName))
            {
                File.Delete(file);
            }
        }
    }

    private IEnumerable<string> GetJsonModels()
    {
        var files = Directory.GetFiles(options.JsonModelsDirectory, "*.json", SearchOption.AllDirectories);
        return files.Select(p => File.ReadAllText(p));
    }

    private void GenerateModels(IEnumerable<DTInterfaceInfo> models)
    {
        var entities = models.Select(m => new ModelEntity(m, options, generatedFiles));
        foreach (var entity in entities)
        {
            entity.GenerateFile();
            generatedFiles.Add($"{entity.Name}.cs");
        }
    }

    private async Task CopyCustomModelsAsync()
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.cs", SearchOption.TopDirectoryOnly);
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileAbsolutePath = $"{options?.OutputDirectory}\\{fileName}";
            File.Copy(file, fileAbsolutePath, true);
            var original = await File.ReadAllTextAsync(fileAbsolutePath);
            var updated = original.Replace("namespace Generator.CustomModels;", $"namespace {options?.Namespace};");
            await File.WriteAllTextAsync(fileAbsolutePath, updated, Encoding.UTF8);
            generatedFiles.Add(fileName);
        }
    }
}
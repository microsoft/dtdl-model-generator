// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

/// <summary>
/// A class used to generate C# POCO models from DTDL json files.
/// </summary>
public class ModelGenerator
{
    private readonly IEnumerable<string> customClasses = new List<string>
    {
        "DateOnlyConverter.cs",
        "DurationConverter.cs",
        "Extensions.cs",
        "MapDateOnlyConverter.cs",
        "MapDurationConverter.cs",
        "ModelHelper.cs",
        "Relationship.cs",
        "RelationshipCollection.cs",
        "RelationshipEqualityComparer.cs",
        "SourceValueAttribute.cs",
        "TwinEqualityComparer.cs",
        "CloudToDeviceMethodOptions.cs",
        "CommandHelper.cs"
    };

    private ModelGeneratorOptions options { get; set; }

    private HashSet<string> generatedFiles = new HashSet<string>();

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
        var bin = Path.Combine(options.Namespace, "bin");
        var obj = Path.Combine(options.Namespace, "obj");
        var noBinOrObjFolders = files.Where(f => !f.Contains(bin) && !f.Contains(obj));
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
        // generatedFiles.Add("AssetComplexCommandRequest.cs");
        var entities = models.Select(m => new ModelEntity(m, options));
        foreach (var entity in entities)
        {
            entity.GenerateFile();
            PopulateGeneratedFilesForEntity(entity);
        }
    }

    private void PopulateGeneratedFilesForEntity(Entity entity)
    {
        if (entity is ClassEntity classEntity)
        {
            generatedFiles.Add(entity.FileName);
            foreach (var propertyContentItem in classEntity.PropertyContent)
            {
                foreach (var producedEntity in propertyContentItem.ProducedEntities)
                {
                    generatedFiles.Add(producedEntity.FileName);
                    if (producedEntity is ClassEntity)
                    {
                        PopulateGeneratedFilesForEntity(producedEntity);
                    }
                }
            }

            foreach (var commandContentItem in classEntity.CommandContent)
            {
                foreach (var producedEntity in commandContentItem.ProducedEntities)
                {
                    generatedFiles.Add(producedEntity.FileName);
                }
            }
        }
    }

    private async Task CopyCustomModelsAsync()
    {
        var assemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly()?.Location);
        var files = Directory.GetFiles(assemblyDirectory ?? string.Empty, "*.cs", SearchOption.TopDirectoryOnly);
        var filteredFiles = files.Where(f => customClasses.Contains(Path.GetFileName(f)));
        foreach (var file in filteredFiles)
        {
            var fileName = Path.GetFileName(file);
            var fileAbsolutePath = Path.Combine(options?.OutputDirectory ?? string.Empty, fileName);
            File.Copy(file, fileAbsolutePath, true);
            var original = await File.ReadAllTextAsync(fileAbsolutePath);
            var updated = original.Replace("namespace Generator.CustomModels;", $"namespace {options?.Namespace};");
            await File.WriteAllTextAsync(fileAbsolutePath, updated, Encoding.UTF8);
            generatedFiles.Add(fileName);
        }
    }
}
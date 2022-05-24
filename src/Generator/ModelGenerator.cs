// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

/// <summary>
/// A class used to generate C# POCO models from DTDL json files.
/// </summary>
public class ModelGenerator
{
    private ModelGeneratorOptions Options { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelGenerator"/> class.
    /// </summary>
    /// <param name="options">The options used to configure behavior of the Generator.</param>
    public ModelGenerator(ModelGeneratorOptions options)
    {
        Options = options;
    }

    /// <summary>
    /// Asynchronously generates C# POCO clases based on DTDL json files
    /// in the directory provided by the generator options and then writes them to
    /// the provided directory set in the generator options.
    /// </summary>
    public async Task GenerateClassesAsync()
    {
        ClearOutputDirectory();
        var files = GetJsonModels();
        var parser = new ModelParser();
        var parsed = await parser.ParseAsync(files).ConfigureAwait(false);
        var models = parsed.Where(i => i.Value.EntityKind == DTEntityKind.Interface)
                           .ToDictionary(p => p.Key, p => (DTInterfaceInfo)p.Value).Values;
        if (Options.IncludeTemplateProject)
        {
            await CopyTemplateProjectAsync();
        }

        await CopyCustomModelsAsync();
        GenerateModels(models);
        GenerateIncludes(models);
    }

    private void ClearOutputDirectory()
    {
        // This loop is only needed to fix a concurrency bug that happens during running debugger
        // The build triggers generation, and then the debugger triggers it again
        while (!DeleteSuccessful(Options.OutputDirectory)) { }
        Directory.CreateDirectory(Options.OutputDirectory);
    }

    private static bool DeleteSuccessful(string directory)
    {
        if (!Directory.Exists(directory))
        {
            return true;
        }

        try
        {
            Directory.Delete(directory, true);
        }
        catch
        {
            return false;
        }

        return true;
    }

    private IEnumerable<string> GetJsonModels()
    {
        var files = Directory.GetFiles(Options.JsonModelsDirectory, "*.json", SearchOption.AllDirectories);
        return files.Select(p => File.ReadAllText(p));
    }

    private void GenerateModels(IEnumerable<DTInterfaceInfo> models)
    {
        var entities = models.Select(m => new ModelEntity(m, Options));
        foreach (var entity in entities)
        {
            entity.GenerateFile();
        }
    }

    private async Task CopyCustomModelsAsync()
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "Custom");
        var files = Directory.GetFiles(dir, "*.cs", SearchOption.TopDirectoryOnly);
        foreach (var file in files)
        {
            var fileName = file.Split("\\").Last();
            var fileAbsolutePath = $"{Options?.OutputDirectory}\\{fileName}";
            File.Copy(file, fileAbsolutePath);
            var original = await File.ReadAllTextAsync(fileAbsolutePath);
            var updated = original.Replace("namespace Generator.CustomModels;", $"namespace {Options?.Namespace};");
            await File.WriteAllTextAsync(fileAbsolutePath, updated, Encoding.UTF8);
        }
    }

    private async Task CopyTemplateProjectAsync()
    {
        var projFile = Directory.GetFiles(Directory.GetCurrentDirectory(), "Generator.TemplateProject.csproj", SearchOption.TopDirectoryOnly).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(projFile))
        {
            return;
        }

        var fileName = projFile.Split("\\").Last();
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            var newCsprojFileName = !string.IsNullOrWhiteSpace(Options?.Namespace) ? $"{Options.Namespace}.csproj" : fileName;
            var outputPath = Path.Combine(Options?.OutputDirectory ?? string.Empty, newCsprojFileName);
            File.Copy(projFile, outputPath);
            var original = await File.ReadAllTextAsync(outputPath);
            var updated = original.Replace("<RootNamespace>Generator.TemplateProject</RootNamespace>", $"<RootNamespace>{Options?.Namespace}</RootNamespace>");
            updated = updated.Replace("<AssemblyName>Generator.TemplateProject</AssemblyName>", $"<AssemblyName>{Options?.Namespace}</AssemblyName>");
            await File.WriteAllTextAsync(outputPath, updated, Encoding.UTF8);
        }
    }

    private void GenerateIncludes(IEnumerable<DTInterfaceInfo> models)
    {
        var roots = models.GroupBy(GetRootModel);
        foreach (var group in roots)
        {
            var key = group.Key.Labels.Last();
            var includes = new HashSet<string> { "none" };
            foreach (var model in group)
            {
                var relationships = model.Contents.Values.Where(c => c.EntityKind == DTEntityKind.Relationship);
                var relationshipNames = relationships.Select(r => r.Name);
                foreach (var relationshipName in relationshipNames)
                {
                    includes.Add(relationshipName);
                }
            }

            if (includes.Count > 1)
            {
                new ModelIncludes(key, includes.ToList(), Options).GenerateFile();
            }
        }
    }

    private Dtmi GetRootModel(DTInterfaceInfo model)
    {
        if (!model.Extends.Any())
        {
            return model.Id;
        }

        return GetRootModel(model.Extends.First());
    }
}
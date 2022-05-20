// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

public class ModelGenerator
{
    private ModelGeneratorOptions Options { get; set; }

    public ModelGenerator(ModelGeneratorOptions options)
    {
        Options = options;
    }

    public void GenerateClasses()
    {
        ClearOutputDirectory();
        var files = GetJsonModels();
        var models = new ModelParser().ParseAsync(files).GetAwaiter().GetResult()
            .Where(i => i.Value.EntityKind == DTEntityKind.Interface)
            .ToDictionary(p => p.Key, p => p.Value as DTInterfaceInfo).Values;
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
        foreach (var model in models.Select(m => new ModelEntity(m, Options)))
        {
            model.GenerateFile();
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
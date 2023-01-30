// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class ModelEntity : ClassEntity
{
    internal Dtmi ModelId { get; set; }

    internal ModelEntity(DTInterfaceInfo interfaceInfo, ModelGeneratorOptions options) : base(options)
    {
        ModelId = interfaceInfo.Id;
        var parents = new Stack<DTInterfaceInfo>();
        foreach (var extend in interfaceInfo.Extends)
        {
            parents.Push(extend);
        }

        while (parents.Any())
        {
            var parent = parents.Pop();
            foreach (var parentContent in parent.Contents)
            {
                interfaceInfo.Contents.TryAdd(parentContent.Key, parentContent.Value);
            }

            foreach (var extend in parent.Extends)
            {
                parents.Push(extend);
            }
        }

        Properties = interfaceInfo.Contents.Values
                            .Where(c => c.EntityKind == DTEntityKind.Property && c is DTPropertyInfo)
                            .Select(c => (DTPropertyInfo)c);
        Name = GetClassName(interfaceInfo.Id);
        Parent = interfaceInfo.Extends.Count() > 0 ? GetClassName(interfaceInfo.Extends.First().Id) : nameof(BasicDigitalTwin);
        FileDirectory = ExtractDirectory(ModelId);
        var contents = interfaceInfo.Contents.Select(c => c.Value);
        Content.AddRange(contents.Select(CreateProperty).Where(p => p != null));
    }

    protected override void WriteConstructor(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}{indent}public {Name}()");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}Metadata.ModelId = ModelId;");
        streamWriter.WriteLine($"{indent}{indent}}}");
    }

    protected override void WriteStaticMembers(StreamWriter streamWriter)
    {
        WriteJsonIgnoreAttribute(streamWriter);
        var setNew = Parent == nameof(BasicDigitalTwin) ? string.Empty : "new ";
        streamWriter.WriteLine($"{indent}{indent}public static {setNew}string ModelId {{ get; }} = \"{ModelId.AbsoluteUri}\";");
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        streamWriter.Write($"message {Name}");
        //if (!string.IsNullOrEmpty(Parent))
        //{
        //    var basicDigitalTwinEquatable = Parent == nameof(BasicDigitalTwin) ? $", IEquatable<{nameof(BasicDigitalTwin)}>" : string.Empty;
        //    streamWriter.Write($" : {Parent}, IEquatable<{Name}>{basicDigitalTwinEquatable}");
        //}

        streamWriter.WriteLine();
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        base.WriteContent(streamWriter);
        //WriteEqualityBlock(streamWriter);
    }

    private string GetClassName(Dtmi id)
    {
        return id.Labels.Last();
    }
}
﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class ModelEntity : ClassEntity
{
    internal Dtmi ModelId { get; set; }

    internal ModelEntity(DTInterfaceInfo interfaceInfo, ModelGeneratorOptions options) : base(options)
    {
        ModelId = interfaceInfo.Id;
        Properties = interfaceInfo.Contents.Values
                            .Where(c => c.Id.Labels.Contains(Name) && c.EntityKind == DTEntityKind.Property && c is DTPropertyInfo)
                            .Select(c => (DTPropertyInfo)c);
        Name = CapitalizeFirstLetter(GetClassName(interfaceInfo.Id));
        Parent = CapitalizeFirstLetter(interfaceInfo.Extends.Count() > 0 ? GetClassName(interfaceInfo.Extends.First().Id) : nameof(BasicDigitalTwin));
        FileDirectory = ExtractDirectory(ModelId);
        var properties = GetContentInfo<DTPropertyInfo>(interfaceInfo);
        PropertyContent.AddRange(properties.Select(CreateProperty));
        var relationships = GetContentInfo<DTRelationshipInfo>(interfaceInfo);
        PropertyContent.AddRange(relationships.Select(CreateProperty));
        var commands = GetContentInfo<DTCommandInfo>(interfaceInfo);
        CommandContent.AddRange(commands.Select((contentInfo) => new Command((DTCommandInfo)contentInfo, Name, Options)));
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
        streamWriter.Write($"{indent}public class {Name}");
        if (!string.IsNullOrEmpty(Parent))
        {
            var basicDigitalTwinEquatable = Parent == nameof(BasicDigitalTwin) ? $", IEquatable<{nameof(BasicDigitalTwin)}>" : string.Empty;
            streamWriter.Write($" : {Parent}, IEquatable<{Name}>{basicDigitalTwinEquatable}");
        }

        streamWriter.WriteLine();
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        base.WriteContent(streamWriter);
        WriteEqualityBlock(streamWriter);
    }

    private string GetClassName(Dtmi id)
    {
        return id.Labels.Last();
    }

    private IEnumerable<DTContentInfo> GetContentInfo<T>(DTInterfaceInfo interfaceInfo) where T : DTContentInfo
    {
        return interfaceInfo.Contents.Select(c => c.Value)
            .Where(c => c.Id.Labels.Contains(Name, StringComparer.OrdinalIgnoreCase) && c is T);
    }
}

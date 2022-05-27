// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class RelationshipCollectionEntity : ClassEntity
{
    internal DTRelationshipInfo RelationshipInfo { get; set; }

    private string Target { get; set; }

    private string NamePrefix { get; set; }

    internal RelationshipCollectionEntity(DTRelationshipInfo info, ModelGeneratorOptions options, IList<string> generatedFiles) : base(options, generatedFiles)
    {
        RelationshipInfo = info;
        Properties = info.Properties;
        var enclosingClass = info.DefinedIn.Labels.Last();
        NamePrefix = $"{enclosingClass}{CapitalizeFirstLetter(RelationshipInfo.Name)}";
        Name = $"{NamePrefix}RelationshipCollection";
        FileDirectory = $"\\Relationship\\{ExtractDirectory(RelationshipInfo.DefinedIn)}\\{enclosingClass}";
        var targetType = RelationshipInfo.Target == null ? nameof(BasicDigitalTwin) : $"{RelationshipInfo.Target.Labels.Last()}";
        Parent = $"RelationshipCollection<{NamePrefix}Relationship, {targetType}>";
        Target = RelationshipInfo.Target == null ? "null" : $"typeof({RelationshipInfo.Target.Labels.Last()})";
    }

    protected override void WriteConstructor(StreamWriter streamWriter)
    {
        var relationshipName = $"{NamePrefix}Relationship";
        streamWriter.WriteLine($"{indent}{indent}public {Name}(IEnumerable<{relationshipName}>? relationships = default) : base(relationships ?? Enumerable.Empty<{relationshipName}>())");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}}}");
    }

    protected override void WriteUsingStatements(StreamWriter streamWriter)
    {
        base.WriteUsingStatements(streamWriter);
        WriteUsingLinq(streamWriter);
    }
}
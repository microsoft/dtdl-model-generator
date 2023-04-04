// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class RelationshipEntity : ClassEntity
{
    internal DTRelationshipInfo RelationshipInfo { get; set; }

    private string SourceType { get; set; }

    private string TargetType { get; set; }

    internal RelationshipEntity(DTRelationshipInfo info, ModelGeneratorOptions options) : base(options)
    {
        RelationshipInfo = info;
        SourceType = info.DefinedIn.Labels.Last();
        Name = $"{SourceType}{CapitalizeFirstLetter(RelationshipInfo.Name)}Relationship";
        FileDirectory = Path.Combine("Relationship", ExtractDirectory(RelationshipInfo.DefinedIn), SourceType);
        TargetType = RelationshipInfo.Target == null ? nameof(BasicDigitalTwin) : RelationshipInfo.Target.Labels.Last();
        Parent = $"Relationship<{TargetType}>";
        PropertyContent.AddRange(info.Properties.Select(p => CreateProperty(p, p.Schema)));
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}public class {Name} : {Parent}, IEquatable<{Name}>");
    }

    protected override void WriteConstructor(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}{indent}public {Name}()");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}Name = \"{RelationshipInfo.Name}\";");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
        streamWriter.WriteLine($"{indent}{indent}public {Name}({SourceType} source, {TargetType} target) : this()");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}InitializeFromTwins(source, target);");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        base.WriteContent(streamWriter);

        WriteEqualsObjectMethod(streamWriter);
        WriteEqualsMethod(streamWriter);
        WriteEqualsOperatorMethod(streamWriter);
        WriteNotEqualsOperatorMethod(streamWriter);
        WriteGetHashCodeMethod(streamWriter);
        WriteBasicRelationshipEqualityMethod(streamWriter);
    }

    private void WriteBasicRelationshipEqualityMethod(StreamWriter streamWriter)
    {
        streamWriter.WriteLine();
        streamWriter.WriteLine($"{indent}{indent}public override bool Equals(BasicRelationship? other)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return Equals(other as {Name}) || new RelationshipEqualityComparer().Equals(this, other);");
        streamWriter.WriteLine($"{indent}{indent}}}");
    }
}
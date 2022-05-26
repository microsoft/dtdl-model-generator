// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

internal class RelationshipEntity : ClassEntity
{
    internal DTRelationshipInfo RelationshipInfo { get; set; }

    private string Target { get; set; }

    private string SourceType { get; set; }

    private string TargetType { get; set; }

    internal RelationshipEntity(DTRelationshipInfo info, ModelGeneratorOptions options, IList<string> generatedFiles) : base(options, generatedFiles)
    {
        RelationshipInfo = info;
        Properties = info.Properties;
        SourceType = info.DefinedIn.Labels.Last();
        Name = $"{SourceType}{CapitalizeFirstLetter(RelationshipInfo.Name)}Relationship";
        FileDirectory = $"\\Relationship\\{ExtractDirectory(RelationshipInfo.DefinedIn)}\\{SourceType}";
        TargetType = RelationshipInfo.Target == null ? nameof(BasicDigitalTwin) : $"{RelationshipInfo.Target.Labels.Last()}";
        Parent = $"Relationship<{TargetType}>";
        Target = RelationshipInfo.Target == null ? "null" : $"typeof({RelationshipInfo.Target.Labels.Last()})";
        Content.AddRange(info.Properties.Select(p => CreateProperty(p, p.Schema)));
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
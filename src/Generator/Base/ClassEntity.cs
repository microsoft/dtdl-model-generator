// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal abstract class ClassEntity : Entity
{
    internal string? Parent { get; set; }

    internal IEnumerable<DTPropertyInfo> Properties { get; set; } = Enumerable.Empty<DTPropertyInfo>();

    internal IEnumerable<DTFieldInfo> Fields { get; set; } = Enumerable.Empty<DTFieldInfo>();

    internal List<Property> Content { get; set; } = new List<Property>();

    internal IList<Property> NonRelationshipProperties => Content.Where(p => !(p is RelationshipProperty)).ToList();

    internal ClassEntity(ModelGeneratorOptions options) : base(options)
    {
    }

    protected override void WriteUsingStatements(StreamWriter streamWriter)
    {
        WriteUsingAzure(streamWriter);
        WriteUsingAdt(streamWriter);
        WriteUsingSystem(streamWriter);
        WriteUsingCollection(streamWriter);
        base.WriteUsingStatements(streamWriter);
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        streamWriter.Write($"message {Name}");
        //if (!string.IsNullOrEmpty(Parent))
        //{
        //    streamWriter.Write($" : {Parent}");
        //}

        streamWriter.WriteLine();
    }

    protected Property CreateProperty(DTNamedEntityInfo entity, DTSchemaInfo schema)
    {
        if (schema is DTMapInfo mapInfo)
        {
            return null;// new MapProperty(entity, mapInfo, Name, Options);
        }

        if (schema is DTEnumInfo enumInfo)
        {
            return null;// new EnumProperty(entity, enumInfo, Name, Options);
        }

        if (schema is DTObjectInfo objectInfo)
        {
            return null;// new ObjectProperty(entity, objectInfo, Name, Options);
        }

        if (schema is DTDurationInfo)
        {
            return null;// new DurationProperty(entity, Options);
        }

        if (schema is DTDateInfo)
        {
            return null;// new DateOnlyProperty(entity, Options);
        }

        try
        {
            return new PrimitiveProperty(entity, schema, Name, Options);
        }
        catch (Exception)
        {
            return null;
        }
    }

    protected Property CreateProperty(DTContentInfo content)
    {
        if (content is DTPropertyInfo property)
        {
            return CreateProperty(property, property.Schema);
        }

        if (content is DTRelationshipInfo relationship)
        {
            return null; //new RelationshipProperty(relationship, Options);
        }

        throw new Exception($"Unsupported content type: {content.EntityKind}");
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        //WriteConstructor(streamWriter);
        //WriteStaticMembers(streamWriter);
        WriteProperties(streamWriter);
    }

    protected virtual void WriteConstructor(StreamWriter streamWriter)
    {
    }

    protected virtual void WriteStaticMembers(StreamWriter streamWriter)
    {
    }

    protected virtual void WriteProperties(StreamWriter streamWriter)
    {
        var count = 1;

        foreach (var property in Content)
        {
            property.WriteTo(streamWriter, count++);
        }
    }

    protected virtual void WriteEqualityBlock(StreamWriter streamWriter, bool isClassObject = false)
    {
        WriteEqualsObjectMethod(streamWriter);
        WriteEqualsMethod(streamWriter, isClassObject);
        WriteEqualsOperatorMethod(streamWriter);
        WriteNotEqualsOperatorMethod(streamWriter);
        WriteGetHashCodeMethod(streamWriter, isClassObject);
        if (Parent == nameof(BasicDigitalTwin))
        {
            WriteBasicDigitalTwinEqualityMethod(streamWriter);
        }
    }

    protected void WriteEqualsObjectMethod(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}{indent}public override bool Equals(object? obj)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return Equals(obj as {Name});");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }

    protected void WriteEqualsMethod(StreamWriter streamWriter, bool isClassObject = false)
    {
        streamWriter.WriteLine($"{indent}{indent}public bool Equals({Name}? other)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        var rootExpr = GetRootEqualityExpression(Parent);
        var start = isClassObject ? $"{indent}{indent}{indent}return other is not null" : $"{indent}{indent}{indent}return other is not null && {rootExpr}";
        var line = new StringBuilder(start);
        if (NonRelationshipProperties.Any())
        {
            var props = NonRelationshipProperties.Select(p => p.Name);
            foreach (var prop in props)
            {
                line.Append($" && {prop} == other.{prop}");
            }
        }

        streamWriter.WriteLine($"{line};");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }

    protected virtual string GetRootEqualityExpression(string? parentType)
    {
        if (parentType == nameof(BasicDigitalTwin))
        {
            return "Id == other.Id && Metadata.ModelId == other.Metadata.ModelId";
        }

        if (parentType?.StartsWith("Relationship<") == true)
        {
            return "Id == other.Id && SourceId == other.SourceId && TargetId == other.TargetId && Target == other.Target && Name == other.Name";
        }

        return "base.Equals(other)";
    }

    protected void WriteEqualsOperatorMethod(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}{indent}public static bool operator ==({Name}? left, {Name}? right)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return EqualityComparer<{Name}?>.Default.Equals(left, right);");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }

    protected void WriteNotEqualsOperatorMethod(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}{indent}public static bool operator !=({Name}? left, {Name}? right)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return !(left == right);");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }

    protected void WriteGetHashCodeMethod(StreamWriter streamWriter, bool isClassObject = false)
    {
        streamWriter.WriteLine($"{indent}{indent}public override int GetHashCode()");
        streamWriter.WriteLine($"{indent}{indent}{{");
        var hashString = new StringBuilder($"{indent}{indent}{indent}return this.CustomHash(");
        if (!isClassObject)
        {
            hashString.Append(GetRootHashExpression(Parent));
        }

        foreach (var prop in NonRelationshipProperties)
        {
            var separator = NonRelationshipProperties.IndexOf(prop) == 0 && isClassObject ? string.Empty : ", ";
            hashString.Append($"{separator}{prop.Name}?.GetHashCode()");
        }

        streamWriter.WriteLine($"{hashString});");
        streamWriter.WriteLine($"{indent}{indent}}}");
    }

    protected virtual string GetRootHashExpression(string? parentType)
    {
        if (parentType == nameof(BasicDigitalTwin))
        {
            return "Id?.GetHashCode(), Metadata?.ModelId?.GetHashCode()";
        }
        else if (parentType?.StartsWith("Relationship<") == true)
        {
            return "Id?.GetHashCode(), SourceId?.GetHashCode(), TargetId?.GetHashCode(), Target?.GetHashCode()";
        }
        else
        {
            return "base.GetHashCode()";
        }
    }

    private void WriteBasicDigitalTwinEqualityMethod(StreamWriter streamWriter)
    {
        streamWriter.WriteLine();
        streamWriter.WriteLine($"{indent}{indent}public bool Equals(BasicDigitalTwin? other)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return Equals(other as {Name}) || new TwinEqualityComparer().Equals(this, other);");
        streamWriter.WriteLine($"{indent}{indent}}}");
    }
}
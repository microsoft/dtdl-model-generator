// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class ObjectEntity : ClassEntity
{
    internal ObjectEntity(DTNamedEntityInfo entityInfo, DTObjectInfo objectInfo, string enclosingEntity, ModelGeneratorOptions options, IList<string> generatedFiles) : base(options, generatedFiles)
    {
        Name = entityInfo.Name;
        if (Name == enclosingEntity)
        {
            Name = $"{Name}Data";
        }

        Fields = objectInfo.Fields;
        AllowOverwrite = true;
        Content.AddRange(objectInfo.Fields.Select(f => CreateProperty(f, f.Schema)));
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}[Serializable]");
        streamWriter.WriteLine($"{indent}public class {Name} : IEquatable<{Name}>");
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        base.WriteContent(streamWriter);
        WriteEqualityBlock(streamWriter, true);
    }
}
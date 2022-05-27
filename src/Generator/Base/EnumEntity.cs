// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class EnumEntity : Entity
{
    internal EnumEntity(ModelGeneratorOptions options, IList<string> generatedFiles) : base(options, generatedFiles)
    {
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        WriteStringEnumConverterAttribute(streamWriter);
        streamWriter.WriteLine($"{indent}public enum {Name}");
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
    }
}
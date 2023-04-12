// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class EnumEntity : Entity
{
    internal EnumEntity(ModelGeneratorOptions options) : base(options)
    {
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        WriteStringEnumConverterAttribute(streamWriter);
        streamWriter.WriteLine($"{indent}public enum {Name}");
    }

    [ExcludeFromCodeCoverage]
    protected override void WriteContent(StreamWriter streamWriter)
    {
    }
}
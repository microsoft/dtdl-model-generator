// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class DateOnlyProperty : Property
{
    internal DateOnlyProperty(DTNamedEntityInfo entity, ModelGeneratorOptions options) : base(options)
    {
        Name = entity.Name;
        JsonName = entity.Name;
        Obsolete = entity.IsObsolete();
        Type = "DateOnly?";
    }

    internal override void WriteTo(StreamWriter streamWriter, int fieldNumber)
    {
        streamWriter.WriteLine($"{indent}{indent}[JsonConverter(typeof(DateOnlyConverter))]");
        base.WriteTo(streamWriter, fieldNumber);
    }
}
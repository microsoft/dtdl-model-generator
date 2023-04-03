// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

using Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator.Exceptions;

internal class PrimitiveProperty : Property
{
    internal PrimitiveProperty(DTNamedEntityInfo entity, DTSchemaInfo schema, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        if (!Types.TryGetNullable(schema.EntityKind, out var type))
        {
            throw new UnsupportedPrimitiveTypeException(schema.EntityKind, entity.Name, enclosingClass);
        }

        Type = type ?? string.Empty;
        Name = JsonName = entity.Name;
        Obsolete = entity.IsObsolete();
    }
}
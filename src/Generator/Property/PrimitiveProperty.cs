// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

internal class PrimitiveProperty : Property
{
    internal PrimitiveProperty(DTPropertyInfo property, string enclosingClass) : this(property, property.Schema, enclosingClass)
    {
    }

    internal PrimitiveProperty(DTNamedEntityInfo entity, DTSchemaInfo schema, string enclosingClass)
    {
        if (!Types.TryGetNullable(schema.EntityKind, out var type))
        {
            throw new Exception($"Unsupported primitive property type: {schema.EntityKind} for {entity.Name} in {enclosingClass}!");
        }

        Type = type;
        Name = JsonName = entity.Name;
        Obsolete = entity.IsObsolete();
    }
}
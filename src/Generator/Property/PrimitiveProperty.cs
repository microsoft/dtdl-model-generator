// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class PrimitiveProperty : Property
{
    internal PrimitiveProperty(DTNamedEntityInfo entity, DTSchemaInfo schema, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        if (!Types.TryGetNullable(schema.EntityKind, out var type))
        {
            throw new Exception($"Unsupported primitive property type: {schema.EntityKind} for {entity.Name} in {enclosingClass}!");
        }

        Type = type ?? string.Empty;
        Name = JsonName = entity.Name;
        Obsolete = entity.IsObsolete();
    }
}
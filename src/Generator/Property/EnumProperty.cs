// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace ADT.Models.Generator;


internal class EnumProperty : Property
{
    internal EnumProperty(DTNamedEntityInfo entityInfo, DTEnumInfo enumInfo, string enclosingClass, ModelGeneratorOptions options)
    {
        var enumEntity = new EnumPropEntity(enumInfo, enclosingClass, options);
        Type = $"{enumEntity.Name}?";
        Name = entityInfo.Name;
        JsonName = entityInfo.Name;
        Obsolete = entityInfo.IsObsolete();
        ProducedEntities.Add(enumEntity);
    }
}
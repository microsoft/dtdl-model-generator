// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace ADT.Models.Generator;

internal class MapProperty : Property
{
    internal MapProperty(DTNamedEntityInfo entity, DTMapInfo map, string enclosingClass, ModelGeneratorOptions options)
    {
        Name = entity.Name;
        JsonName = entity.Name;
        Types.TryGetNonNullable(map.MapKey.Schema.EntityKind, out var mapKey);
        string mapValue;
        if (map.MapValue.Schema is DTEnumInfo enumInfo)
        {
            var enumEntity = new EnumPropEntity(enumInfo, enclosingClass, options);
            mapValue = enumEntity.Name;
            ProducedEntities.Add(enumEntity);
        }
        else if (map.MapValue.Schema is DTObjectInfo objectInfo)
        {
            var objectEntity = new ObjectEntity(map.MapValue, objectInfo, enclosingClass, options);
            mapValue = objectEntity.Name;
            ProducedEntities.Add(objectEntity);
        }
        else
        {
            Types.TryGetNonNullable(map.MapValue.Schema.EntityKind, out mapValue);
        }

        Type = $"IDictionary<{mapKey.TrimEnd('?')}, {mapValue.TrimEnd('?')}>";
        Obsolete = entity.IsObsolete();
    }
}
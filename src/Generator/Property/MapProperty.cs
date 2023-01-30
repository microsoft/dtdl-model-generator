// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class MapProperty : Property
{
    private readonly string? mapValue;

    internal MapProperty(DTNamedEntityInfo entity, DTMapInfo map, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        Name = entity.Name;
        JsonName = entity.Name;
        Types.TryGetNonNullable(map.MapKey.Schema.EntityKind, out var mapKey);
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

        Type = $"IDictionary<{mapKey?.TrimEnd('?')}, {mapValue?.TrimEnd('?')}>?";
        Obsolete = entity.IsObsolete();
    }

    internal override void WriteTo(StreamWriter streamWriter, int fieldNumber)
    {
        if (mapValue == nameof(TimeSpan))
        {
            streamWriter.WriteLine($"{indent}{indent}[JsonConverter(typeof(MapDurationConverter))]");
        }

        if (mapValue == nameof(DateOnly))
        {
            streamWriter.WriteLine($"{indent}{indent}[JsonConverter(typeof(MapDateOnlyConverter))]");
        }

        base.WriteTo(streamWriter, fieldNumber);
    }
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class EnumProperty : Property
{
    internal EnumProperty(DTNamedEntityInfo entityInfo, DTEnumInfo enumInfo, string enclosingClass, ModelGeneratorOptions options, IList<string> generatedFiles) : base(options)
    {
        var enumEntity = new EnumPropEntity(enumInfo, enclosingClass, options, generatedFiles);
        Type = $"{enumEntity.Name}?";
        Name = entityInfo.Name;
        JsonName = entityInfo.Name;
        Obsolete = entityInfo.IsObsolete();
        ProducedEntities.Add(enumEntity);
    }
}
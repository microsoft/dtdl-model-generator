// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ObjectProperty : Property
{
    internal ObjectProperty(DTNamedEntityInfo entityInfo, DTObjectInfo objectInfo, string enclosingClass, ModelGeneratorOptions options, IList<string> generatedFiles) : base(options)
    {
        Name = entityInfo.Name;
        JsonName = entityInfo.Name;
        if (Name == enclosingClass)
        {
            Name = $"{Name}Value";
        }

        var objectEntity = new ObjectEntity(entityInfo, objectInfo, enclosingClass, options, generatedFiles);
        Type = $"{objectEntity.Name}?";
        Obsolete = entityInfo.IsObsolete();
        ProducedEntities.Add(objectEntity);
    }
}
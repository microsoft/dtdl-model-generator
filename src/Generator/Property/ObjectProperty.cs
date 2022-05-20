// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace ADT.Models.Generator
{
    using Microsoft.Azure.DigitalTwins.Parser;

    internal class ObjectProperty : Property
    {
        internal ObjectProperty(DTNamedEntityInfo entityInfo, DTObjectInfo objectInfo, string enclosingClass, ModelGeneratorOptions options)
        {
            Name = entityInfo.Name;
            JsonName = entityInfo.Name;
            if (Name == enclosingClass)
            {
                Name = $"{Name}Value";
            }

            var objectEntity = new ObjectEntity(entityInfo, objectInfo, enclosingClass, options);
            Type = objectEntity.Name;
            Obsolete = entityInfo.IsObsolete();
            ProducedEntities.Add(objectEntity);
        }
    }
}
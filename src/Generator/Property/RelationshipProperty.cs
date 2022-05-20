// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

internal class RelationshipProperty : Property
{
    internal RelationshipProperty(DTRelationshipInfo relationship, ModelGeneratorOptions options)
    {
        var relationshipEntity = new RelationshipEntity(relationship, options);
        var relationshipCollectionEntity = new RelationshipCollectionEntity(relationship, options);
        Type = relationshipCollectionEntity.Name;
        Name = relationship.Name;
        JsonIgnore = true;
        Initialized = true;
        Obsolete = relationship.IsObsolete();
        ProducedEntities.Add(relationshipEntity);
        ProducedEntities.Add(relationshipCollectionEntity);
    }
}
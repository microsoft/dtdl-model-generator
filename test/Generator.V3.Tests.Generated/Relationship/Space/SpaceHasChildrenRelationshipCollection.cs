// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests.Generated
{
    using Azure;
    using Azure.DigitalTwins.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System.Linq;

    public class SpaceHasChildrenRelationshipCollection : RelationshipCollection<SpaceHasChildrenRelationship, Space>
    {
        public SpaceHasChildrenRelationshipCollection(IEnumerable<SpaceHasChildrenRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<SpaceHasChildrenRelationship>())
        {
        }
    }
}
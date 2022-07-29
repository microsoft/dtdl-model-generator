// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using Generator.Tests.Generated.test;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Linq;

namespace Generator.Tests.Generated.test.relationship.space
{

    public class SpaceHasChildrenRelationshipCollection : RelationshipCollection<SpaceHasChildrenRelationship, Space>
    {
        public SpaceHasChildrenRelationshipCollection(IEnumerable<SpaceHasChildrenRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<SpaceHasChildrenRelationship>())
        {
        }
    }
}
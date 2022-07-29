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

namespace Generator.Tests.Generated.test.space.relationship.building
{

    public class BuildingHasAddressRelationshipCollection : RelationshipCollection<BuildingHasAddressRelationship, Address>
    {
        public BuildingHasAddressRelationshipCollection(IEnumerable<BuildingHasAddressRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<BuildingHasAddressRelationship>())
        {
        }
    }
}
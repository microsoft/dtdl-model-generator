// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated
{
    using Azure;
    using Azure.DigitalTwins.Core;
    using System;
    using System.Runtime;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System.Linq;

    public class BuildingHasAddressRelationshipCollection : RelationshipCollection<BuildingHasAddressRelationship, Address>
    {
        public BuildingHasAddressRelationshipCollection(IEnumerable<BuildingHasAddressRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<BuildingHasAddressRelationship>())
        {
        }
    }
}
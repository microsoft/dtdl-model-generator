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

namespace Generator.Tests.Generated.test.relationship.address
{

    public class AddressHasStateRelationshipCollection : RelationshipCollection<AddressHasStateRelationship, StateProvince>
    {
        public AddressHasStateRelationshipCollection(IEnumerable<AddressHasStateRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<AddressHasStateRelationship>())
        {
        }
    }
}
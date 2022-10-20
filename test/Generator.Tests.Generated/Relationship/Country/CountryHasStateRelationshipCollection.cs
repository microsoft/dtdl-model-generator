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

    public class CountryHasStateRelationshipCollection : RelationshipCollection<CountryHasStateRelationship, StateProvince>
    {
        public CountryHasStateRelationshipCollection(IEnumerable<CountryHasStateRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<CountryHasStateRelationship>())
        {
        }
    }
}
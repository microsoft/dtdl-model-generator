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

    public class AddressHasCountryRelationshipCollection : RelationshipCollection<AddressHasCountryRelationship, Country>
    {
        public AddressHasCountryRelationshipCollection(IEnumerable<AddressHasCountryRelationship>? relationships = default) : base(relationships ?? Enumerable.Empty<AddressHasCountryRelationship>())
        {
        }
    }
}
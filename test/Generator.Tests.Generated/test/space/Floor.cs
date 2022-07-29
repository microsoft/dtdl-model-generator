// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using Generator.Tests.Generated.test;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Generator.Tests.Generated.test.space
{

    public class Floor : Space, IEquatable<Floor>
    {
        public Floor()
        {
            Metadata.ModelId = ModelId;
        }
        [JsonIgnore]
        public static new string ModelId { get; } = "dtmi:test:Space:Floor;1";
        [JsonPropertyName("logicalOrder")]
        public int? LogicalOrder { get; set; }
        [JsonPropertyName("regionId")]
        public string? RegionId { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Floor);
        }

        public bool Equals(Floor? other)
        {
            return other is not null && base.Equals(other) && LogicalOrder == other.LogicalOrder && RegionId == other.RegionId;
        }

        public static bool operator ==(Floor? left, Floor? right)
        {
            return EqualityComparer<Floor?>.Default.Equals(left, right);
        }

        public static bool operator !=(Floor? left, Floor? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(base.GetHashCode(), LogicalOrder?.GetHashCode(), RegionId?.GetHashCode());
        }
    }
}
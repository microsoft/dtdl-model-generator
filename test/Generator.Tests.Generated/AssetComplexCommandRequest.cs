// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated
{
    using Azure;
    using Azure.DigitalTwins.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [Serializable]
    public class AssetComplexCommandRequest : IEquatable<AssetComplexCommandRequest>
    {
        [JsonPropertyName("field1")]
        public double? Field1 { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as AssetComplexCommandRequest);
        }

        public bool Equals(AssetComplexCommandRequest? other)
        {
            return other is not null && Field1 == other.Field1;
        }

        public static bool operator ==(AssetComplexCommandRequest? left, AssetComplexCommandRequest? right)
        {
            return EqualityComparer<AssetComplexCommandRequest?>.Default.Equals(left, right);
        }

        public static bool operator !=(AssetComplexCommandRequest? left, AssetComplexCommandRequest? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(Field1?.GetHashCode());
        }
    }
}
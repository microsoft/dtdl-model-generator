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
    public class TestModelData : IEquatable<TestModelData>
    {
        [JsonPropertyName("testField")]
        public bool? TestField { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as TestModelData);
        }

        public bool Equals(TestModelData? other)
        {
            return other is not null && TestField == other.TestField;
        }

        public static bool operator ==(TestModelData? left, TestModelData? right)
        {
            return EqualityComparer<TestModelData?>.Default.Equals(left, right);
        }

        public static bool operator !=(TestModelData? left, TestModelData? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(TestField?.GetHashCode());
        }
    }
}
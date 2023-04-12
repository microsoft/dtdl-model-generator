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

    public class TestModel : BasicDigitalTwin, IEquatable<TestModel>, IEquatable<BasicDigitalTwin>
    {
        public TestModel()
        {
            Metadata.ModelId = ModelId;
        }
        [JsonIgnore]
        public static string ModelId { get; } = "dtmi:test:TestModel;1";
        [JsonPropertyName("TestModel")]
        public TestModelData? TestModelValue { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as TestModel);
        }

        public bool Equals(TestModel? other)
        {
            return other is not null && Id == other.Id && Metadata.ModelId == other.Metadata.ModelId && TestModelValue == other.TestModelValue;
        }

        public static bool operator ==(TestModel? left, TestModel? right)
        {
            return EqualityComparer<TestModel?>.Default.Equals(left, right);
        }

        public static bool operator !=(TestModel? left, TestModel? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(Id?.GetHashCode(), Metadata?.ModelId?.GetHashCode(), TestModelValue?.GetHashCode());
        }

        public bool Equals(BasicDigitalTwin? other)
        {
            return Equals(other as TestModel) || new TwinEqualityComparer().Equals(this, other);
        }
    }
}
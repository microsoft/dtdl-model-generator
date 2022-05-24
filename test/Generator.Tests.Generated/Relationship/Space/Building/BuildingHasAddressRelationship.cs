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

    public class BuildingHasAddressRelationship : Relationship<Address>, IEquatable<BuildingHasAddressRelationship>
    {
        public BuildingHasAddressRelationship()
        {
            Name = "hasAddress";
        }

        public BuildingHasAddressRelationship(Building source, Address target) : this()
        {
            InitializeFromTwins(source, target);
        }


        private const string addressType = nameof(addressType);

        [JsonPropertyName(addressType)]
        public BuildingHasAddressRelationshipAddressType? AddressType { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as BuildingHasAddressRelationship);
        }

        public bool Equals(BuildingHasAddressRelationship? other)
        {
            var targetsAreEqual = (Target is null && other?.Target is null) || (!(Target is null) && !(other?.Target is null) && Target == other.Target);
            return !(other is null) && Id == other.Id && SourceId == other.SourceId && TargetId == other.TargetId && targetsAreEqual && AddressType == other.AddressType;
        }

        public static bool operator ==(BuildingHasAddressRelationship left, BuildingHasAddressRelationship right)
        {
            return EqualityComparer<BuildingHasAddressRelationship>.Default.Equals(left, right);
        }

        public static bool operator !=(BuildingHasAddressRelationship left, BuildingHasAddressRelationship right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(Id?.GetHashCode(), SourceId?.GetHashCode(), TargetId?.GetHashCode(), Target?.GetHashCode(), AddressType?.GetHashCode());
        }

        public override bool Equals(BasicRelationship? other)
        {
            return Equals(other as BuildingHasAddressRelationship) || new RelationshipEqualityComparer().Equals(this, other);
        }
    }
}
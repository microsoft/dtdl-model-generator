// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests.Generated
{
    using Azure;
    using Azure.DigitalTwins.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    public class AssetIsLocatedInRelationship : Relationship<Space>, IEquatable<AssetIsLocatedInRelationship>
    {
        public AssetIsLocatedInRelationship()
        {
            Name = "isLocatedIn";
        }

        public AssetIsLocatedInRelationship(Asset source, Space target) : this()
        {
            InitializeFromTwins(source, target);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AssetIsLocatedInRelationship);
        }

        public bool Equals(AssetIsLocatedInRelationship? other)
        {
            return other is not null && Id == other.Id && SourceId == other.SourceId && TargetId == other.TargetId && Target == other.Target && Name == other.Name;
        }

        public static bool operator ==(AssetIsLocatedInRelationship? left, AssetIsLocatedInRelationship? right)
        {
            return EqualityComparer<AssetIsLocatedInRelationship?>.Default.Equals(left, right);
        }

        public static bool operator !=(AssetIsLocatedInRelationship? left, AssetIsLocatedInRelationship? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(Id?.GetHashCode(), SourceId?.GetHashCode(), TargetId?.GetHashCode(), Target?.GetHashCode());
        }

        public override bool Equals(BasicRelationship? other)
        {
            return Equals(other as AssetIsLocatedInRelationship) || new RelationshipEqualityComparer().Equals(this, other);
        }
    }
}
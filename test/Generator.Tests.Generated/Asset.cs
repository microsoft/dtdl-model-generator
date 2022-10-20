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

    public class Asset : BasicDigitalTwin, IEquatable<Asset>, IEquatable<BasicDigitalTwin>
    {
        public Asset()
        {
            Metadata.ModelId = ModelId;
        }
        [JsonIgnore]
        public static string ModelId { get; } = "dtmi:test:Asset;1";
        [JsonPropertyName("assetTag")]
        public string? AssetTag { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("serialNumber")]
        public string? SerialNumber { get; set; }
        [JsonConverter(typeof(DurationConverter))]
        [JsonPropertyName("maintenanceInterval")]
        public TimeSpan? MaintenanceInterval { get; set; }
        [JsonConverter(typeof(DateOnlyConverter))]
        [JsonPropertyName("installedOn")]
        public DateOnly? InstalledOn { get; set; }
        [JsonConverter(typeof(MapDurationConverter))]
        [JsonPropertyName("runtimeDurations")]
        public IDictionary<string, TimeSpan>? RuntimeDurations { get; set; }
        [JsonConverter(typeof(MapDateOnlyConverter))]
        [JsonPropertyName("runtimeDetails")]
        public IDictionary<string, DateOnly>? RuntimeDetails { get; set; }
        [JsonIgnore]
        public AssetIsLocatedInRelationshipCollection IsLocatedIn { get; set; } = new AssetIsLocatedInRelationshipCollection();
        public override bool Equals(object? obj)
        {
            return Equals(obj as Asset);
        }

        public bool Equals(Asset? other)
        {
            return other is not null && Id == other.Id && Metadata.ModelId == other.Metadata.ModelId && AssetTag == other.AssetTag && Name == other.Name && SerialNumber == other.SerialNumber && MaintenanceInterval == other.MaintenanceInterval && InstalledOn == other.InstalledOn && RuntimeDurations == other.RuntimeDurations && RuntimeDetails == other.RuntimeDetails;
        }

        public static bool operator ==(Asset? left, Asset? right)
        {
            return EqualityComparer<Asset?>.Default.Equals(left, right);
        }

        public static bool operator !=(Asset? left, Asset? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(Id?.GetHashCode(), Metadata?.ModelId?.GetHashCode(), AssetTag?.GetHashCode(), Name?.GetHashCode(), SerialNumber?.GetHashCode(), MaintenanceInterval?.GetHashCode(), InstalledOn?.GetHashCode(), RuntimeDurations?.GetHashCode(), RuntimeDetails?.GetHashCode());
        }

        public bool Equals(BasicDigitalTwin? other)
        {
            return Equals(other as Asset) || new TwinEqualityComparer().Equals(this, other);
        }
    }
}
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

    public class POI : Area, IEquatable<POI>
    {
        public POI()
        {
            Metadata.ModelId = ModelId;
        }
        [JsonIgnore]
        public static new string ModelId { get; } = "dtmi:test:Space:Area:POI;1";
        [JsonPropertyName("category")]
        public Category? Category { get; set; }
        [JsonPropertyName("genericRules")]
        public GenericRules? GenericRules { get; set; }
        [JsonPropertyName("scheduleRules")]
        public ScheduleRules? ScheduleRules { get; set; }
        [JsonPropertyName("amenities")]
        public IDictionary<string, bool>? Amenities { get; set; }
        [JsonPropertyName("weeklyOperationHours")]
        public IDictionary<string, OperationHour>? WeeklyOperationHours { get; set; }
        [JsonPropertyName("subStatus")]
        public POISubStatus? SubStatus { get; set; }
        [JsonPropertyName("mediaList")]
        public IDictionary<string, Media>? MediaList { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as POI);
        }

        public bool Equals(POI? other)
        {
            var categoryEquals = (Category is null && other?.Category is null) || (!(Category is null) && !(other?.Category is null) && Category == other.Category);
            var genericRulesEquals = (GenericRules is null && other?.GenericRules is null) || (!(GenericRules is null) && !(other?.GenericRules is null) && GenericRules == other.GenericRules);
            var scheduleRulesEquals = (ScheduleRules is null && other?.ScheduleRules is null) || (!(ScheduleRules is null) && !(other?.ScheduleRules is null) && ScheduleRules == other.ScheduleRules);
            return !(other is null) && base.Equals(other) && categoryEquals && genericRulesEquals && scheduleRulesEquals && Amenities == other.Amenities && WeeklyOperationHours == other.WeeklyOperationHours && SubStatus == other.SubStatus && MediaList == other.MediaList;
        }

        public static bool operator ==(POI left, POI right)
        {
            return EqualityComparer<POI>.Default.Equals(left, right);
        }

        public static bool operator !=(POI left, POI right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(base.GetHashCode(), Category?.GetHashCode(), GenericRules?.GetHashCode(), ScheduleRules?.GetHashCode(), Amenities?.GetHashCode(), WeeklyOperationHours?.GetHashCode(), SubStatus?.GetHashCode(), MediaList?.GetHashCode());
        }
    }
}
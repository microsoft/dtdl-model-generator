// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V2.Tests.Generated
{
    using Azure;
    using Microsoft.Azure.Devices;
    using Azure.DigitalTwins.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;

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
        public static async Task<(int status, AssetComplexCommandResponse? enumOutput)> ComplexCommandAsync(ServiceClient serviceClient, string deviceId, string moduleId, AssetComplexCommandRequest objInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<AssetComplexCommandRequest, AssetComplexCommandResponse?>(serviceClient, deviceId, moduleId, "complexCommand", objInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, AssetComplexCommandResponse? enumOutput)> ComplexCommandAsync(ServiceClient serviceClient, string deviceId, AssetComplexCommandRequest objInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<AssetComplexCommandRequest, AssetComplexCommandResponse?>(serviceClient, deviceId, null, "complexCommand", objInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<int> SimpleCommandAsync(ServiceClient serviceClient, string deviceId, string moduleId, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync(serviceClient, deviceId, moduleId, "simpleCommand", options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<int> SimpleCommandAsync(ServiceClient serviceClient, string deviceId, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync(serviceClient, deviceId, null, "simpleCommand", options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, string? stringOutput)> PrimitiveReqResCommandAsync(ServiceClient serviceClient, string deviceId, string moduleId, bool? booleanInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<bool?, string?>(serviceClient, deviceId, moduleId, "primitiveReqResCommand", booleanInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, string? stringOutput)> PrimitiveReqResCommandAsync(ServiceClient serviceClient, string deviceId, bool? booleanInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<bool?, string?>(serviceClient, deviceId, null, "primitiveReqResCommand", booleanInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<int> PrimitiveReqCommandAsync(ServiceClient serviceClient, string deviceId, string moduleId, int? integerInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<int?>(serviceClient, deviceId, moduleId, "primitiveReqCommand", integerInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<int> PrimitiveReqCommandAsync(ServiceClient serviceClient, string deviceId, int? integerInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<int?>(serviceClient, deviceId, null, "primitiveReqCommand", integerInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, double? doubleOutput)> PrimitiveResCommandAsync(ServiceClient serviceClient, string deviceId, string moduleId, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<double?>(serviceClient, deviceId, moduleId, "primitiveResCommand", options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, double? doubleOutput)> PrimitiveResCommandAsync(ServiceClient serviceClient, string deviceId, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<double?>(serviceClient, deviceId, null, "primitiveResCommand", options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, IDictionary<string, string>? mapOutput)> MapReqResCommandAsync(ServiceClient serviceClient, string deviceId, string moduleId, IDictionary<string, string>? mapInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<IDictionary<string, string>?, IDictionary<string, string>?>(serviceClient, deviceId, moduleId, "mapReqResCommand", mapInput, options, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<(int status, IDictionary<string, string>? mapOutput)> MapReqResCommandAsync(ServiceClient serviceClient, string deviceId, IDictionary<string, string>? mapInput, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await CommandHelper.SendCommandAsync<IDictionary<string, string>?, IDictionary<string, string>?>(serviceClient, deviceId, null, "mapReqResCommand", mapInput, options, cancellationToken).ConfigureAwait(false);
        }

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
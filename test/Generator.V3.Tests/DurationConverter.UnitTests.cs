// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests;

[TestClass]
public class DurationConverterUnitTests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    [TestMethod]
    public void PopulatedDurationPropertyDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"maintenanceInterval\":\"P90DT8H7M6S\",\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", MaintenanceInterval = new TimeSpan(90, 8, 7, 6) };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.MaintenanceInterval, deserializedAsset?.MaintenanceInterval);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void NullDurationPropertyDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"maintenanceInterval\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", MaintenanceInterval = null };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.MaintenanceInterval, deserializedAsset?.MaintenanceInterval);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void PopulatedDurationPropertySerializesCorrectly()
    {
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"maintenanceInterval\":\"P90DT8H7M6S\",\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var asset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", MaintenanceInterval = new TimeSpan(90, 8, 7, 6) };
        var actualJson = JsonSerializer.Serialize(asset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }

    [TestMethod]
    public void NullDurationPropertySerializesCorrectly()
    {
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"squareMeter\":2,\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"maintenanceInterval\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var asset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", MaintenanceInterval = null };
        var actualJson = JsonSerializer.Serialize<Asset>(asset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }
}
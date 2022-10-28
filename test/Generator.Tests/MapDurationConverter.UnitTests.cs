// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class MapDurationConverterUnitTests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    [TestMethod]
    public void PopulatedMapDurationPropertyDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDurations\":{{\"Monday\":\"PT1H\",\"Wednesday\":\"PT2H30M\"}},\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDurations = new Dictionary<string, TimeSpan>()
            {
                ["Monday"] = new TimeSpan(1, 0, 0),
                ["Wednesday"] = new TimeSpan(2, 30, 0)
            }
        };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.RuntimeDurations["Monday"], deserializedAsset?.RuntimeDurations!["Monday"]);
        Assert.AreEqual(expectedAsset.RuntimeDurations["Wednesday"], deserializedAsset?.RuntimeDurations!["Wednesday"]);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void NullMapDurationPropertyDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDurations\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDurations = null
        };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.RuntimeDurations, expectedAsset.RuntimeDurations);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void PopulatedMapDurationPropertySerializesCorrectly()
    {
        var asset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDurations = new Dictionary<string, TimeSpan>()
            {
                ["Monday"] = new TimeSpan(1, 0, 0),
                ["Wednesday"] = new TimeSpan(2, 30, 0)
            }
        };
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDurations\":{{\"Monday\":\"PT1H\",\"Wednesday\":\"PT2H30M\"}},\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var actualJson = JsonSerializer.Serialize<Asset>(asset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }

    [TestMethod]
    public void NullMapDurationPropertySerializesCorrectly()
    {
        var asset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDurations = null
        };
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDurations\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var actualJson = JsonSerializer.Serialize<Asset>(asset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests;

[TestClass]
public class MapDurationConverterUnitTests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    private const string testFirstWeekDay = "Monday";
    private const string testSecondWeekDay = "Wednesday";

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
                { testFirstWeekDay, new TimeSpan(1, 0, 0) },
                { testSecondWeekDay, new TimeSpan(2, 30, 0) }
            }
        };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.RuntimeDurations[testFirstWeekDay], deserializedAsset?.RuntimeDurations![testFirstWeekDay]);
        Assert.AreEqual(expectedAsset.RuntimeDurations[testSecondWeekDay], deserializedAsset?.RuntimeDurations![testSecondWeekDay]);
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
                { testFirstWeekDay, new TimeSpan(1, 0, 0) },
                { testSecondWeekDay, new TimeSpan(2, 30, 0) }
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
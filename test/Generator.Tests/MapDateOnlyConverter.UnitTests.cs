// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class MapDateOnlyConverterUnitTests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    [TestMethod]
    public void PopulatedMapDateOnlyPropertyDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDetails\":{{\"Friday\":\"2020-03-10\",\"Sunday\":\"2020-03-12\"}},\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDetails = new Dictionary<string, DateOnly>()
            {
                ["Friday"] = new DateOnly(2020, 03, 10),
                ["Sunday"] = new DateOnly(2020, 03, 12)
            }
        };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.RuntimeDetails["Friday"], deserializedAsset?.RuntimeDetails["Friday"]);
        Assert.AreEqual(expectedAsset.RuntimeDetails["Sunday"], deserializedAsset?.RuntimeDetails["Sunday"]);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void NullMapDateOnlyPropertyDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDetails\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDetails = null
        };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(json, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.RuntimeDetails, deserializedAsset?.RuntimeDetails);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void PopulatedMapDateOnlyPropertySerializesCorrectly()
    {
        var asset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDetails = new Dictionary<string, DateOnly>()
            {
                ["Friday"] = new DateOnly(2020, 03, 10),
                ["Sunday"] = new DateOnly(2020, 03, 12)
            }
        };
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDetails\":{{\"Friday\":\"2020-03-10\",\"Sunday\":\"2020-03-12\"}},\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var actualJson = JsonSerializer.Serialize<Asset>(asset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }

    [TestMethod]
    public void NullMapDateOnlyPropertySerializesCorrectly()
    {
        var asset = new Asset
        {
            Id = "d8985302-4ee1-4a10-b2f5-e854e1682422",
            AssetTag = "ABC123",
            Name = "Test Asset",
            SerialNumber = "SN12345",
            RuntimeDetails = null
        };
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"runtimeDetails\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var actualJson = JsonSerializer.Serialize<Asset>(asset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }
}
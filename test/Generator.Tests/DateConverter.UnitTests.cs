// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class DateConverterUnitTests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    [TestMethod]
    public void PopulatedDatePropertyDeserializesCorrectly()
    {
        var inputJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"installedOn\":\"2020-03-10\",\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", InstalledOn = new DateOnly(2020, 03, 10) };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(inputJson, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.InstalledOn, deserializedAsset?.InstalledOn);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void NullDatePropertyDeserializesCorrectly()
    {
        var inputJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"installedOn\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var expectedAsset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", InstalledOn = null };
        var deserializedAsset = JsonSerializer.Deserialize<Asset>(inputJson, options);
        Assert.AreEqual(expectedAsset.Id, deserializedAsset?.Id);
        Assert.AreEqual(expectedAsset.Name, deserializedAsset?.Name);
        Assert.AreEqual(expectedAsset.SerialNumber, deserializedAsset?.SerialNumber);
        Assert.AreEqual(expectedAsset.InstalledOn, deserializedAsset?.InstalledOn);
        Assert.AreEqual(expectedAsset.Metadata.ModelId, deserializedAsset?.Metadata.ModelId);
    }

    [TestMethod]
    public void PopulatedDatePropertySerializesCorrectly()
    {
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"installedOn\":\"2020-03-10\",\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var inputAsset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", InstalledOn = new DateOnly(2020, 03, 10) };
        var actualJson = JsonSerializer.Serialize(inputAsset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }

    [TestMethod]
    public void NullDatePropertySerializesCorrectly()
    {
        var expectedJson = $"{{\"$dtId\":\"d8985302-4ee1-4a10-b2f5-e854e1682422\",\"assetTag\":\"12345\",\"squareMeter\":2,\"name\":\"Test Asset\",\"serialNumber\":\"SN12345\",\"installedOn\":null,\"$metadata\":{{\"$model\":\"{Asset.ModelId}\"}}}}";
        var inputAsset = new Asset { Id = "d8985302-4ee1-4a10-b2f5-e854e1682422", AssetTag = "ABC123", Name = "Test Asset", SerialNumber = "SN12345", InstalledOn = null };
        var actualJson = JsonSerializer.Serialize<Asset>(inputAsset, options);
        AssertHelper.AssertJsonEquivalent(expectedJson, actualJson);
    }
}
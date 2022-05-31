// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

[TestClass]
public class ModelsUnitTests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    [TestInitialize]
    public async Task InitializeAsync()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var jsonDir = Path.Combine(currentDir, "TestDtdlModels");
        var outDir = PathHelper.GetCombinedFullPath(currentDir, "../../../../Generator.Tests.Generated");
        var options = new ModelGeneratorOptions
        {
            OutputDirectory = outDir,
            Namespace = "Generator.Tests.Generated",
            JsonModelsDirectory = jsonDir
        };

        var generator = new ModelGenerator(options);
        await generator.GenerateClassesAsync().ConfigureAwait(false);
        await Task.Delay(500);
        AssertHelper.AssertFilesGenerated(jsonDir, options.OutputDirectory);
    }

    [TestMethod]
    public void ModelEqualityWorksAsExpected()
    {
        var id = Guid.NewGuid().ToString();
        var regionId = Guid.NewGuid().ToString();
        var floor1 = new Floor { Id = id, LogicalOrder = 1, Status = SpaceStatus.Active, RegionId = regionId };
        var floor2 = new Floor { Id = id, LogicalOrder = 1, Status = SpaceStatus.Active, RegionId = regionId };
        var floorAsBase = floor2 as Space;
        var equalsWithOperator = floorAsBase == floor1;
        var equalsWithMethod = floorAsBase.Equals(floor1);
        var equalsWithMethodReverse = floor1.Equals(floorAsBase);
        Assert.IsTrue(equalsWithOperator);
        Assert.IsTrue(equalsWithMethod);
        Assert.IsTrue(equalsWithMethodReverse);
    }

    [TestMethod]
    public void GetHashCodeReturnsTheSameHashForEqualObjects()
    {
        var space1 = new Space { Id = Guid.NewGuid().ToString() };
        var space2 = new Space { Id = space1.Id };
        var areEqual = space1.Equals(space2);
        var hash1 = space1.GetHashCode();
        var hash2 = space2.GetHashCode();
        var hashEquals = hash1 == hash2;
        Assert.IsTrue(areEqual);
        Assert.AreEqual(hash1, hash2);
        Assert.AreEqual(space1, space2);
    }

    [TestMethod]
    public void GetHashCodeReturnsTheSameHashForTwinsWithObjectPropertiesThatShouldBeEqual()
    {
        var poi1 = new POI { Id = Guid.NewGuid().ToString(), ScheduleRules = new ScheduleRules { IsAccessAfterHoursAllowed = true } };
        var poi2 = new POI { Id = poi1.Id, ScheduleRules = new ScheduleRules { IsAccessAfterHoursAllowed = true } };
        var areEqual = poi1.Equals(poi2);
        var hashEquals = poi1.GetHashCode() == poi2.GetHashCode();
        Assert.IsTrue(areEqual);
        Assert.IsTrue(hashEquals);
    }

    [TestMethod]
    public void CanAccessModelStaticMetadata()
    {
        var staticValue = Space.ModelId;
        Assert.IsTrue(staticValue.StartsWith("dtmi:test:Space;"));
    }

    [TestMethod]
    public void CanAccessModelInstanceMetadata()
    {
        BasicDigitalTwin model = new Space();
        var instanceValue = model.Metadata.ModelId;
        Assert.IsTrue(instanceValue.StartsWith("dtmi:test:Space;"));
    }

    [TestMethod]
    public void CanAccessModelSubclassInstanceMetadata()
    {
        BasicDigitalTwin model = new Building();
        var instanceValue = model.Metadata.ModelId;
        Assert.IsTrue(instanceValue.StartsWith("dtmi:test:Space:Building;"));
    }

    [TestMethod]
    public void CanAccessModelSubclassStaticMetadata()
    {
        var staticValue = Building.ModelId;
        Assert.IsTrue(staticValue.StartsWith("dtmi:test:Space:Building;"));
    }

    [TestMethod]
    public void CanAccessRelationshipInstanceMetadata()
    {
        BasicRelationship relationship = new BuildingHasAddressRelationship();
        var instanceValue = relationship.Name;
        Assert.AreEqual("hasAddress", instanceValue);
    }

    [TestMethod]
    public void ModelRelationshipPropertiesAreInitializedOnlyOnce()
    {
        var buildingmodel = new Building();
        var address1 = new Address { Street1 = "123 St", Street2 = "abc", County = "king", Zipcode = "1001" };
        var address2 = new Address { Street1 = "456 St", Street2 = "def", County = "snohomish", Zipcode = "2001" };

        buildingmodel.HasAddress = new BuildingHasAddressRelationshipCollection(new List<BuildingHasAddressRelationship>
            {
                new BuildingHasAddressRelationship(buildingmodel, address1),
                new BuildingHasAddressRelationship(buildingmodel, address2),
            });

        Assert.AreEqual(2, buildingmodel.HasAddress.Count());
        Assert.AreEqual("123 St", buildingmodel.HasAddress.First()?.Target?.Street1);
    }

    [TestMethod]
    public void ModelSerializesCorrectly()
    {
        var guid = Guid.NewGuid().ToString();
        var building = new Building { Name = "TestBuilding", Id = guid };
        var expectedJsonWithNulls = $"{{\"$metadata\":{{\"$model\":\"{Building.ModelId}\"}},\"businessEntityNumber\":null,\"number\":null,\"shortName\":null,\"squareMeter\":null,\"rationalSortKey\":null,\"regionId\":null,\"startOfBusinessTime\":null,\"endOfBusinessTime\":null,\"businessEntityName\":null,\"amenities\":null,\"externalId\":null,\"name\":\"TestBuilding\",\"roomKey\":null,\"friendlyName\":null,\"description\":null,\"squareFootArea\":null,\"capabilities\":null,\"status\":null,\"physicalSpace\":null,\"$dtId\":\"{guid}\",\"$etag\":null}}";
        var expectedJsonWithoutNulls = $"{{\"$metadata\":{{\"$model\":\"{Building.ModelId}\"}},\"name\":\"TestBuilding\",\"$dtId\":\"{guid}\"}}";
        var actualJsonWithNulls = JsonSerializer.Serialize(building);
        AssertJsonEquivalent(expectedJsonWithNulls, actualJsonWithNulls);

        var actualJsonWithoutNulls = JsonSerializer.Serialize(building, options);
        AssertJsonEquivalent(expectedJsonWithoutNulls, actualJsonWithoutNulls);
    }

    [TestMethod]
    public void ModelDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d6eee516-8e60-4845-993d-0e35c5af677e\",\"name\":\"TestBuilding\",\"status\":\"Active\",\"$metadata\":{{\"$model\":\"{Space.ModelId}\"}}}}";
        var expectedSpace = new Space { Id = "d6eee516-8e60-4845-993d-0e35c5af677e", Name = "TestBuilding", Status = SpaceStatus.Active };
        var deserializedSpace = JsonSerializer.Deserialize<Space>(json, options);
        Assert.AreEqual(expectedSpace.Id, deserializedSpace?.Id);
        Assert.AreEqual(expectedSpace.Name, deserializedSpace?.Name);
        Assert.AreEqual(expectedSpace.Status, deserializedSpace?.Status);
        Assert.AreEqual(expectedSpace.Metadata.ModelId, deserializedSpace?.Metadata.ModelId);
    }

    [TestMethod]
    public void ModelSubclassDeserializesCorrectly()
    {
        var json = $"{{\"$dtId\":\"d6eee516-8e60-4845-993d-0e35c5af677e\",\"number\":0,\"squareMeter\":2,\"name\":\"TestBuilding\",\"squareFootArea\":0,\"status\":\"Active\",\"$metadata\":{{\"$model\":\"{Building.ModelId}\"}}}}";
        var expectedBuilding = new Building { Id = "d6eee516-8e60-4845-993d-0e35c5af677e", Name = "TestBuilding", Status = SpaceStatus.Active, Number = 2 };
        var deserializedBuilding = JsonSerializer.Deserialize<Building>(json, options);
        Assert.AreEqual(expectedBuilding.Id, deserializedBuilding?.Id);
        Assert.AreEqual(expectedBuilding.Name, deserializedBuilding?.Name);
        Assert.AreEqual(expectedBuilding.Status, deserializedBuilding?.Status);
        Assert.AreEqual(expectedBuilding.Metadata.ModelId, deserializedBuilding?.Metadata.ModelId);
    }

    [TestMethod]
    public void RelationshipSerializesCorrectly()
    {
        var sourceId = Guid.NewGuid();
        var targetId = Guid.NewGuid();
        var relationshipId = Guid.NewGuid();
        var expectedJson = $"{{\"$relationshipName\":\"hasAddress\",\"$relationshipId\":\"{relationshipId}\",\"$sourceId\":\"{sourceId}\",\"$targetId\":\"{targetId}\"}}";
        var relationship = new BuildingHasAddressRelationship { Id = relationshipId.ToString(), SourceId = sourceId.ToString(), TargetId = targetId.ToString() };
        var actualJson = JsonSerializer.Serialize(relationship, options);
        AssertJsonEquivalent(expectedJson, actualJson);
    }

    [TestMethod]
    public void RelationshipDeserializesCorrectly()
    {
        var sourceId = Guid.NewGuid();
        var targetId = Guid.NewGuid();
        var relationshipId = Guid.NewGuid();
        var json = $"{{\"$relationshipId\":\"{relationshipId}\",\"$sourceId\":\"{sourceId}\",\"$relationshipName\":\"hasAddress\",\"$targetId\":\"{targetId}\",\"addressType\":\"Mailing\"}}";
        var expectedRelationship = new BuildingHasAddressRelationship { Id = relationshipId.ToString(), SourceId = sourceId.ToString(), TargetId = targetId.ToString(), AddressType = BuildingHasAddressRelationshipAddressType.Mailing };
        var deserializedRelationship = JsonSerializer.Deserialize<BuildingHasAddressRelationship>(json);
        Assert.AreEqual(expectedRelationship.Id, deserializedRelationship?.Id);
        Assert.AreEqual(expectedRelationship.SourceId, deserializedRelationship?.SourceId);
        Assert.AreEqual(expectedRelationship.TargetId, deserializedRelationship?.TargetId);
        Assert.AreEqual(expectedRelationship.Name, deserializedRelationship?.Name);
        Assert.AreEqual(expectedRelationship.AddressType, deserializedRelationship?.AddressType);

        var deserializedBaseRelationship = JsonSerializer.Deserialize<BasicRelationship>(json);
        Assert.AreEqual(expectedRelationship.Id, deserializedBaseRelationship?.Id);
        Assert.AreEqual(expectedRelationship.SourceId, deserializedBaseRelationship?.SourceId);
        Assert.AreEqual(expectedRelationship.TargetId, deserializedBaseRelationship?.TargetId);
        Assert.AreEqual(expectedRelationship.Name, deserializedBaseRelationship?.Name);
    }

    private void AssertJsonEquivalent(string expected, string actual)
    {
        using var expectedToken = JsonDocument.Parse(expected);
        using var actualToken = JsonDocument.Parse(actual);
        expectedToken.DeepEquals(actualToken);
    }
}
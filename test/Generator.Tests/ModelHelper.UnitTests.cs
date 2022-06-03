// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

#nullable disable warnings
[TestClass]
public class ModelHelperTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InvalidSelectorThrowsException()
    {
        ModelHelper.SelectJsonProperty<Building>(b => b.FriendlyName.ToString(), out var _);
    }

    [TestMethod]
    public void PropertyWithoutJsonNameReturnNull()
    {
        var prop = ModelHelper.SelectJsonProperty<Building>(b => b.FriendlyName.Length, out var type);
        Assert.IsNull(prop);
    }

    [TestMethod]
    public void CanSelectProperty()
    {
        var prop = ModelHelper.SelectJsonProperty<Building>(b => b.FriendlyName, out var type);
        Assert.AreEqual("friendlyName", prop);
        Assert.AreEqual(typeof(Building), type);
        prop = ModelHelper.SelectJsonProperty<Building>(b => b.BusinessEntityName, out type);
        Assert.AreEqual("businessEntityName", prop);
        Assert.AreEqual(typeof(Building), type);
        prop = ModelHelper.SelectJsonProperty<Building>(b => b.Id, out type);
        Assert.AreEqual("$dtId", prop);
        Assert.AreEqual(typeof(Building), type);
    }

    [TestMethod]
    public void CanSelectEnumProperty()
    {
        var prop = ModelHelper.SelectJsonProperty<Building>(b => b.Status, out var type);
        Assert.AreEqual("status", prop);
        Assert.AreEqual(typeof(Building), type);
    }

    [TestMethod]
    public void CanExtractJsonAttributeNameFromTwinProperty()
    {
        var json = new Address().GetPropertyJsonName(c => c.Street1);
        Assert.AreEqual("street1", json);
    }

    [TestMethod]
    public void CanExtractEnumValueAttributeFromEnum()
    {
        Assert.AreEqual("Mailing", BuildingHasAddressRelationshipAddressType.Mailing.GetEnumValue());
        Assert.AreEqual("Mailing", new BuildingHasAddressRelationship { AddressType = BuildingHasAddressRelationshipAddressType.Mailing }.AddressType.GetEnumValue());
    }

    [TestMethod]
    public void CanExtractSourceValueAttributeFromEnum()
    {
        Assert.AreEqual("1", BuildingHasAddressRelationshipAddressType.Mailing.GetSourceValue());
        Assert.AreEqual("1", new BuildingHasAddressRelationship { AddressType = BuildingHasAddressRelationshipAddressType.Mailing }.AddressType.GetSourceValue());
    }

    [TestMethod]
    public void CanExtractDisplayAttributeFromEnum()
    {
        Assert.AreEqual("Active", SpaceStatus.Active.GetDisplayAttribute()?.Name);
        Assert.AreEqual("active", SpaceStatus.Active.GetDisplayAttribute()?.Description);
        Assert.AreEqual("Active", new Space { Status = SpaceStatus.Active }.Status.GetDisplayAttribute()?.Name);
        Assert.AreEqual("active", new Space { Status = SpaceStatus.Active }.Status.GetDisplayAttribute()?.Description);
    }
}
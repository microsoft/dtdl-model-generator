// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests;

using Moq;
using Microsoft.Azure.Devices;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System;

[TestClass]
public class CommandHelperTest
{
    private string deviceId = string.Empty;
    private string moduleId = string.Empty;
    private Mock<ServiceClient> mockserviceClient = new Mock<ServiceClient>();
    private CloudToDeviceMethodResult mockCloudToDeviceMethodResult = new CloudToDeviceMethodResult();
    private Random numberGenerator = new Random();
    private int randomId;
    private int randomStatus;

    [TestInitialize]
    public void Initialize()
    {
        mockserviceClient = new Mock<ServiceClient>();
        randomId = numberGenerator.Next(100);
        randomStatus = numberGenerator.Next(10);
        mockCloudToDeviceMethodResult.Status = randomStatus;
    }

    [TestMethod]
    public async Task CanInvokePrimitiveReqResCommand()
    {
        bool request = true;
        string expectedResponse = "stringOutput";
        mockCloudToDeviceMethodResult.SetCloudToDeviceMethodResultPayload(expectedResponse);
        deviceId = $"testDeviceId{randomId}";
        moduleId = $"testModlueId{randomId}";
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (int status, string? response) = await Asset.PrimitiveReqResCommandAsync(mockserviceClient.Object, deviceId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse, response);
        Assert.AreEqual(randomStatus, status);

        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (status, response) = await Asset.PrimitiveReqResCommandAsync(mockserviceClient.Object, deviceId, moduleId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse, response);
        Assert.AreEqual(randomStatus, status);
    }

    [TestMethod]
    public async Task CanInvokePrimitiveReqCommand()
    {
        int request = 0;
        deviceId = $"testDeviceId{randomId}";
        moduleId = $"testModlueId{randomId}";
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        int status = await Asset.PrimitiveReqCommandAsync(mockserviceClient.Object, deviceId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(randomStatus, status);

        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        status = await Asset.PrimitiveReqCommandAsync(mockserviceClient.Object, deviceId, moduleId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(randomStatus, status);
    }

    [TestMethod]
    public async Task CanInvokePrimitiveResCommand()
    {
        int expectedResponse = 1;
        mockCloudToDeviceMethodResult.SetCloudToDeviceMethodResultPayload(1);
        deviceId = $"testDeviceId{randomId}";
        moduleId = $"testModlueId{randomId}";
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (int status, double? response) = await Asset.PrimitiveResCommandAsync(mockserviceClient.Object, deviceId, new CloudToDeviceMethodOptions()).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse, response);
        Assert.AreEqual(randomStatus, status);

        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (status, response) = await Asset.PrimitiveResCommandAsync(mockserviceClient.Object, deviceId, moduleId, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse, response);
        Assert.AreEqual(randomStatus, status);
    }

    [TestMethod]
    public async Task CanInvokeMapReqResCommand()
    {
        var expectedResponse = new Dictionary<string, string>() { { "testKey", "testValue" } };
        mockCloudToDeviceMethodResult.SetCloudToDeviceMethodResultPayload(expectedResponse);
        var request = new Dictionary<string, string>()
        {
            { string.Empty, string.Empty },
            { "a", string.Empty }
        };
        deviceId = $"testDeviceId{randomId}";
        moduleId = $"testModlueId{randomId}";
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (int status, var response) = await Asset.MapReqResCommandAsync(mockserviceClient.Object, deviceId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse.Count, response?.Count);
        Assert.AreEqual(0, expectedResponse.Except(response ?? new Dictionary<string, string>()).Count());
        Assert.AreEqual(randomStatus, status);

        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (status, response) = await Asset.MapReqResCommandAsync(mockserviceClient.Object, deviceId, moduleId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse.Count, response?.Count);
        Assert.AreEqual(0, expectedResponse.Except(response ?? new Dictionary<string, string>()).Count());
        Assert.AreEqual(randomStatus, status);
    }

    [TestMethod]
    public async Task CanInvokeSimpleCommand()
    {
        deviceId = $"testDeviceId{randomId}";
        moduleId = $"testModlueId{randomId}";
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        int status = await Asset.SimpleCommandAsync(mockserviceClient.Object, deviceId, new CloudToDeviceMethodOptions()).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(randomStatus, status);

        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        status = await Asset.SimpleCommandAsync(mockserviceClient.Object, deviceId, moduleId, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(randomStatus, status);
    }

    [TestMethod]
    public async Task CanInvokeComplexCommand()
    {
        var expectedResponse = AssetComplexCommandResponse.input1;
        mockCloudToDeviceMethodResult.SetCloudToDeviceMethodResultPayload(expectedResponse);
        var request = new AssetComplexCommandRequest();
        deviceId = $"testDeviceId{randomId}";
        moduleId = $"testModlueId{randomId}";
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        (int status, var response) = await Asset.ComplexCommandAsync(mockserviceClient.Object, deviceId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        mockserviceClient.Setup(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCloudToDeviceMethodResult);
        Assert.AreEqual(expectedResponse, response);
        Assert.AreEqual(randomStatus, status);

        (status, response) = await Asset.ComplexCommandAsync(mockserviceClient.Object, deviceId, moduleId, request, null).ConfigureAwait(false);
        mockserviceClient.Verify(serviceClient => serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, It.IsAny<CloudToDeviceMethod>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(expectedResponse, response);
        Assert.AreEqual(randomStatus, status);
    }
}

internal static class CloudToDeviceMethodResultHelper
{
    private static PropertyInfo? payloadProperty = typeof(CloudToDeviceMethodResult).GetProperty("Payload", BindingFlags.Instance | BindingFlags.NonPublic);

    public static CloudToDeviceMethodResult SetCloudToDeviceMethodResultPayload<T>(this CloudToDeviceMethodResult result, T payload)
    {
        var json = JsonSerializer.Serialize(payload);
        payloadProperty?.SetValue(result, new JRaw(json));
        return result;
    }
}
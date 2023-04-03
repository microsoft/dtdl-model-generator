// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.CustomModels;

using Microsoft.Azure.Devices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

internal static class CommandHelper
{
    internal static async Task<(int status, TResponse? response)> SendCommandAsync<TRequest, TResponse>(ServiceClient serviceClient, string deviceId, string? moduleId, string methodName, TRequest request, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
    {
        var payloadJson = JsonSerializer.Serialize(request);
        var cloudToDeviceMethod = BuildCloudToDeviceMethod(methodName, options, payloadJson);
        var cloudToDeviceMethodResult = await InvokeDeviceMethodAsync(serviceClient, deviceId, moduleId, cloudToDeviceMethod, cancellationToken).ConfigureAwait(false);
        var response = JsonSerializer.Deserialize<TResponse>(cloudToDeviceMethodResult.GetPayloadAsJson());
        return (cloudToDeviceMethodResult.Status, response);
    }

    internal static async Task<(int status, TResponse? response)> SendCommandAsync<TResponse>(ServiceClient serviceClient, string deviceId, string? moduleId, string methodName, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
    {
        var cloudToDeviceMethod = BuildCloudToDeviceMethod(methodName, options, null);
        var cloudToDeviceMethodResult = await InvokeDeviceMethodAsync(serviceClient, deviceId, moduleId, cloudToDeviceMethod, cancellationToken).ConfigureAwait(false);
        var response = JsonSerializer.Deserialize<TResponse>(cloudToDeviceMethodResult.GetPayloadAsJson());
        return (cloudToDeviceMethodResult.Status, response);
    }

    internal static async Task<int> SendCommandAsync<TRequest>(ServiceClient serviceClient, string deviceId, string? moduleId, string methodName, TRequest request, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
    {
        var payloadJson = JsonSerializer.Serialize(request);
        var cloudToDeviceMethod = BuildCloudToDeviceMethod(methodName, options, payloadJson);
        var cloudToDeviceMethodResult = await InvokeDeviceMethodAsync(serviceClient, deviceId, moduleId, cloudToDeviceMethod, cancellationToken).ConfigureAwait(false);
        return cloudToDeviceMethodResult.Status;
    }

    internal static async Task<int> SendCommandAsync(ServiceClient serviceClient, string deviceId, string? moduleId, string methodName, CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)
    {
        var cloudToDeviceMethod = BuildCloudToDeviceMethod(methodName, options, null);
        var cloudToDeviceMethodResult = await InvokeDeviceMethodAsync(serviceClient, deviceId, moduleId, cloudToDeviceMethod, cancellationToken).ConfigureAwait(false);
        return cloudToDeviceMethodResult.Status;
    }

    private static CloudToDeviceMethod BuildCloudToDeviceMethod(string methodName, CloudToDeviceMethodOptions? options, string? payloadJson)
    {
        var methodInvocation = new CloudToDeviceMethod(methodName);
        if (options?.ConnectionTimeout is not null)
        {
            methodInvocation.ConnectionTimeout = options.ConnectionTimeout.Value;
        }
        if (options?.ResponseTimeout is not null)
        {
            methodInvocation.ResponseTimeout = options.ResponseTimeout.Value;
        }
        if (payloadJson is not null)
        {
            methodInvocation.SetPayloadJson(payloadJson);
        }
        return methodInvocation;
    }

    private static async Task<CloudToDeviceMethodResult> InvokeDeviceMethodAsync(ServiceClient serviceClient, string deviceId, string? moduleId, CloudToDeviceMethod methodInvocation, CancellationToken cancellationToken)
    {
        if (moduleId is not null)
        {
            return await serviceClient.InvokeDeviceMethodAsync(deviceId, moduleId, methodInvocation, cancellationToken).ConfigureAwait(false);
        }
        return await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation, cancellationToken).ConfigureAwait(false);
    }
}
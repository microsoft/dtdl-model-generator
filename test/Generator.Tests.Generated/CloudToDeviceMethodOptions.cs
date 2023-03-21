// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated;

using System;

/// <summary>
/// Encapsulates an Azure IoT Hub <see href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.azure.devices.cloudtodevicemethod">CloudToDeviceMethod</see> properties.
/// </summary>
public class CloudToDeviceMethodOptions
{
    public CloudToDeviceMethodOptions() { }

    internal CloudToDeviceMethodOptions(TimeSpan? connectionTimeout, TimeSpan? responseTimeout)
    {
        ConnectionTimeout = connectionTimeout;
        ResponseTimeout = responseTimeout;
    }

    internal TimeSpan? ConnectionTimeout { get; set; }

    internal TimeSpan? ResponseTimeout { get; set; }
}
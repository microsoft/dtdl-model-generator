// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests.Generated;

using System;

/// <summary>
/// Encapsulates an Azure IoT Hub <see href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.azure.devices.cloudtodevicemethod">CloudToDeviceMethod</see> properties.
/// </summary>
public class CloudToDeviceMethodOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CloudToDeviceMethodOptions"/> class.
    /// </summary>
    public CloudToDeviceMethodOptions() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CloudToDeviceMethodOptions"/> class.
    /// </summary>
    /// <param name="connectionTimeout">A TimeSpan of connectionTimeout to initalize the <see cref="CloudToDeviceMethodOptions"/> with.</param>
    /// <param name="responseTimeout">A TimeSpan of responseTimeout to initalize the <see cref="CloudToDeviceMethodOptions"/> with.</param>
    internal CloudToDeviceMethodOptions(TimeSpan? connectionTimeout, TimeSpan? responseTimeout)
    {
        ConnectionTimeout = connectionTimeout;
        ResponseTimeout = responseTimeout;
    }

    /// <summary>
    /// Gets or sets the ConnectionTimeout.
    /// </summary>
    internal TimeSpan? ConnectionTimeout { get; set; }

    /// <summary>
    /// Gets or sets the ResponseTimeout.
    /// </summary>
    internal TimeSpan? ResponseTimeout { get; set; }
}
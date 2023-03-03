// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated;

using System;

/// <summary>
/// Encapsulates IOT hub CloudToDeviceMethod options.
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
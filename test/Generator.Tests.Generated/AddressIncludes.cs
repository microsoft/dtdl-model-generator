// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Abstractions.DigitalTwins
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System;

    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AddressIncludes
    {
        None = 0,
        HasState = 1 << 0,
        HasCity = 1 << 1,
        HasCountry = 1 << 2
    }
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Abstractions.DigitalTwins
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System;

    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SpaceIncludes
    {
        None = 0,
        HasChildren = 1 << 0,
        HasParent = 1 << 1,
        HasAddress = 1 << 2
    }
}
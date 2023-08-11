// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests.Generated
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AssetComplexCommandResponse
    {
        [EnumMember(Value = "input1"), Display(Name = "input1"), SourceValue(Value = "1")]
        input1
    }
}
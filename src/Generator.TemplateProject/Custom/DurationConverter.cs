// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.CustomModels;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// Converts an ISO 8601 duration into a TimeSpan.
/// </summary>
public class DurationConverter : JsonConverter<TimeSpan>
{
    /// <inheritdoc/>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if(string.IsNullOrWhiteSpace(value))
        {
            return TimeSpan.Zero;
        }
        return XmlConvert.ToTimeSpan(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(XmlConvert.ToString(value));
    }
}
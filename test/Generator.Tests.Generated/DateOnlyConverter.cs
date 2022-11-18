// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// Converts an RFC 3339 Date into a DateOnly.
/// </summary>
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat = "yyyy-MM-dd";
    /// <inheritdoc/>
    public override DateOnly Read(ref Utf8JsonReader reader,
                            Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if(string.IsNullOrWhiteSpace(value))
        {
            return new DateOnly();
        }
        return DateOnly.Parse(value);   
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateOnly value,
                                        JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(serializationFormat));
}
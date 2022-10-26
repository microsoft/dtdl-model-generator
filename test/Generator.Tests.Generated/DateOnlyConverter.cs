﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Runtime;

/// <summary>
/// Converts an RFC 3339 Date into a DateOnly.
/// </summary>
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateOnlyConverter"/> class.
    /// </summary>
    public DateOnlyConverter() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateOnlyConverter"/> class.
    /// </summary>
    /// <param name="serializationFormat">Serialization format. </param>
    public DateOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    /// <inheritdoc/>
    public override DateOnly Read(ref Utf8JsonReader reader,
                            Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateOnly value,
                                        JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(serializationFormat));
}
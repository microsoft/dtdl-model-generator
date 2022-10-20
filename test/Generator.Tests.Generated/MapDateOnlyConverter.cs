// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// Converts a map of RFC 3339 into an IDictionary&lt;string, DateOnly&lt;.
/// </summary>
public class MapDateOnlyConverter : JsonConverter<IDictionary<string, DateOnly>>
{
    private readonly string serializationFormat;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapDateOnlyConverter"/> class.
    /// </summary>
    public MapDateOnlyConverter() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapDateOnlyConverter"/> class.
    /// </summary>
    /// <param name="serializationFormat">Serialization format. </param>
    public MapDateOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    /// <inheritdoc/>
    public override IDictionary<string, DateOnly> Read(ref Utf8JsonReader reader,
                                                 Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("First token was not the start of a JSON object.");
        }

        var dictionary = new Dictionary<string, DateOnly>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return dictionary;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var key = reader.GetString();

            reader.Read();
            if (reader.TokenType == JsonTokenType.Null)
            {
                continue;
            }

#pragma warning disable CS8604 // Possible null reference argument.
            var value = DateOnly.Parse(reader.GetString());
            dictionary.Add(key, value);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        throw new JsonException("Final token was not the end of a JSON object.");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<string, DateOnly> dictionary,
                          JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach ((string key, DateOnly value) in dictionary)
        {
            writer.WritePropertyName(options.PropertyNamingPolicy?.ConvertName(key) ?? key);
            writer.WriteStringValue(value.ToString(serializationFormat));
        }

        writer.WriteEndObject();
    }
}
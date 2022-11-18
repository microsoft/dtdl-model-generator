// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.CustomModels;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// Converts a map of RFC 3339 into an <see cref="IDictionary{String, DateOnly}"/>.
/// </summary>
public class MapDateOnlyConverter : JsonConverter<IDictionary<string, DateOnly>>
{
    private readonly string serializationFormat = "yyyy-MM-dd";
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
            if (reader.TokenType == JsonTokenType.Null || string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            var value = reader.GetString();

            if(!string.IsNullOrWhiteSpace(value))
            {
                var parsedValue = DateOnly.Parse(value);
                dictionary.Add(key, parsedValue);
            }
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
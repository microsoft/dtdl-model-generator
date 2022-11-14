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
/// Converts a map of ISO 8601 durations into an IDictionary&lt;string, TimeSpan&lt;.
/// </summary>
public class MapDurationConverter : JsonConverter<IDictionary<string, TimeSpan>>
{
    /// <inheritdoc/>
    public override IDictionary<string, TimeSpan> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("First token was not the start of a JSON object.");
        }

        var dictionary = new Dictionary<string, TimeSpan>();

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

            if (!string.IsNullOrWhiteSpace(value))
            {
                var parsedValue = XmlConvert.ToTimeSpan(value);
                dictionary.Add(key, parsedValue);
            }
        }

        throw new JsonException("Final token was not the end of a JSON object.");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<string, TimeSpan> dictionary, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach ((string key, TimeSpan value) in dictionary)
        {
            writer.WritePropertyName(options.PropertyNamingPolicy?.ConvertName(key) ?? key);
            writer.WriteStringValue(XmlConvert.ToString(value));
        }

        writer.WriteEndObject();
    }
}
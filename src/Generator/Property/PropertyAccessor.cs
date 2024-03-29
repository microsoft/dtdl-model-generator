﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal abstract class PropertyAccessor : Writable
{
    private string? body;

    internal string? Body
    {
        get => body;
        set
        {
            body = ReindentText(TrimWhitespace(value));
        }
    }

    internal PropertyAccessor(ModelGeneratorOptions options) : base(options)
    {
    }

    private string? TrimWhitespace(string? text) => !string.IsNullOrWhiteSpace(text) ? text.Trim() : text;

    private string? ReindentText(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        var reindented = Regex.Replace(text, @"(\r\n|\n)\s*", $"$1{indent}{indent}{indent}{indent}");
        return $"{indent}{indent}{indent}{indent}" + reindented;
    }
}
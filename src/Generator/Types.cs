// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal static class Types
{
    private static readonly IDictionary<DTEntityKind, string> nullablePropertyTypeMapping = new Dictionary<DTEntityKind, string>
        {
            { DTEntityKind.Integer, "int?" },
            { DTEntityKind.Boolean, "bool?" },
            { DTEntityKind.DateTime, "DateTime?" },
            { DTEntityKind.Time, "object?" },
            { DTEntityKind.Float, "float?" },
            { DTEntityKind.Double, "double?" },
            { DTEntityKind.String, "string?" }
        };

    private static readonly IDictionary<DTEntityKind, string> nonNullablePropertyTypeMapping = new Dictionary<DTEntityKind, string>
        {
            { DTEntityKind.Integer, "int" },
            { DTEntityKind.Boolean, "bool" },
            { DTEntityKind.DateTime, "DateTime" },
            { DTEntityKind.Time, "object" },
            { DTEntityKind.Float, "float" },
            { DTEntityKind.Double, "double" },
            { DTEntityKind.String, "string" },
            { DTEntityKind.Duration, "TimeSpan" }
        };

    internal static bool TryGetNullable(DTEntityKind entityKind, out string? type)
    {
        return nullablePropertyTypeMapping.TryGetValue(entityKind, out type);
    }

    internal static bool TryGetNonNullable(DTEntityKind entityKind, out string? type)
    {
        return nonNullablePropertyTypeMapping.TryGetValue(entityKind, out type);
    }
}
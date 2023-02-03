// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal static class Types
{
    private static readonly IDictionary<DTEntityKind, string> nullablePropertyTypeMapping = new Dictionary<DTEntityKind, string>
        {
            { DTEntityKind.Integer, "google.protobuf.Int32Value" },
            { DTEntityKind.Long, "google.protobuf.Int64Value" },
            { DTEntityKind.Boolean, "google.protobuf.BoolValue" },
            { DTEntityKind.Float, "google.protobuf.FloatValue" },
            { DTEntityKind.Double, "google.protobuf.DoubleValue" },
            { DTEntityKind.String, "google.protobuf.StringValue" },
            //{ DTEntityKind.DateTime, "DateTime?" },
            //{ DTEntityKind.Time, "object?" },
        };

    private static readonly IDictionary<DTEntityKind, string> nonNullablePropertyTypeMapping = new Dictionary<DTEntityKind, string>
        {
            { DTEntityKind.Integer, "int32" },
            { DTEntityKind.Long, "int64" },
            { DTEntityKind.Boolean, "bool" },
            { DTEntityKind.Float, "float" },
            { DTEntityKind.Double, "double" },
            { DTEntityKind.String, "string" },
            { DTEntityKind.DateTime, "google.protobuf.Timestamp" },
            //{ DTEntityKind.Date, "DateOnly" },
            //{ DTEntityKind.Time, "object" },
            { DTEntityKind.Duration, "google.protobuf.Duration" },
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
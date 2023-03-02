// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class PropertySetter : PropertyAccessor
{

    internal bool IsInit { get; }

    internal PropertySetter(ModelGeneratorOptions options) : base(options)
    {
    }

    internal void WriteTo(StreamWriter writer)
    {
        writer.Write(IsInit ? "init" : "set");
        writer.Write("; ");
    }
}
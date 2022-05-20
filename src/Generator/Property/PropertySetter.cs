// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

internal class PropertySetter : PropertyAccessor
{
    internal AccessModifier? AccessModifier { get; set; }

    internal bool IsInit { get; set; }

    internal void WriteTo(StreamWriter writer)
    {
        if (AccessModifier != null)
        {
            writer.Write(AccessModifier?.Serialize());
            writer.Write(" ");
        }

        writer.Write(IsInit ? "init" : "set");

        if (Body == null)
        {
            writer.Write("; ");
        }
        else
        {
            writer.WriteLine();
            writer.WriteLine($"{indent}{indent}{indent}{{");
            writer.WriteLine(Body);
            writer.WriteLine($"{indent}{indent}{indent}}}");
        }
    }
}
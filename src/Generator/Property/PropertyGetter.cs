// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class PropertyGetter : PropertyAccessor
{
    internal AccessModifier? AccessModifier { get; set; }

    internal PropertyGetter(ModelGeneratorOptions options) : base(options)
    {
    }

    internal void WriteTo(StreamWriter writer)
    {
        if (AccessModifier != null)
        {
            writer.Write(AccessModifier?.Serialize());
            writer.Write(" ");
        }

        writer.Write("get");

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
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ModelIncludes : EnumEntity
{
    internal List<string> Relationships { get; set; }

    internal ModelIncludes(string rootClass, List<string> relationships, ModelGeneratorOptions options, IList<string> generatedFiles) : base(options, generatedFiles)
    {
        Name = $"{rootClass}Includes";
        Relationships = relationships;
    }

    protected override void WriteNamespace(StreamWriter streamWriter)
    {
        streamWriter.WriteLine("namespace Microsoft.DigitalWorkplace.Integration.Abstractions.DigitalTwins");
    }

    protected override void WriteSignature(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}[Flags]");
        base.WriteSignature(streamWriter);
    }

    protected override void WriteUsingStatements(StreamWriter streamWriter)
    {
        base.WriteUsingStatements(streamWriter);
        WriteUsingSystem(streamWriter);
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        var i = 0;
        foreach (var relationship in Relationships)
        {
            var isLastItem = Relationships.IndexOf(relationship) == Relationships.Count - 1;
            var flagEnumDelimiter = i == 0 ? "= 0" : $"= {1} << {i - 1}";
            var comma = isLastItem ? string.Empty : ",";
            streamWriter.WriteLine($"{indent}{indent}{CapitalizeFirstLetter(relationship)} {flagEnumDelimiter}{comma}");
            i++;
        }
    }
}
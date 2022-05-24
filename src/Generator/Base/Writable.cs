// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

internal abstract class Writable
{
    protected const string indent = "    ";

    internal ModelGeneratorOptions Options { get; set; }

    internal Writable(ModelGeneratorOptions options)
    {
        Options = options;
    }

    protected void WriteJsonIgnoreAttribute(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}{indent}[JsonIgnore]");
    }

    protected static string CapitalizeFirstLetter(string word)
    {
        return char.ToUpper(word[0]) + word.Substring(1);
    }

    protected static string LowercaseFirstLetter(string word)
    {
        return char.ToLower(word[0]) + word.Substring(1);
    }
}
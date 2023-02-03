// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

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

    protected static string ConvertToProtobufNamingConvention(string word)
    {
        return string.Concat(word.Select((x, i) => char.IsUpper(x) && i > 0 ? "_" + char.ToLower(x) : x.ToString()));
    }

    protected static string ConvertEnumValueToProtobufNamingConvention(string word)
    {
        return string.Concat(word.Select((x, i) => char.IsUpper(x) && i > 0 ? "_" + char.ToUpper(x) : x.ToString())).ToUpper();
    }

    protected static string LowercaseFirstLetter(string word)
    {
        return char.ToLower(word[0]) + word.Substring(1);
    }
}
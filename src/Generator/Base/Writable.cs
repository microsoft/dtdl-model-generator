// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace ADT.Models.Generator
{
    using System.IO;

    internal abstract class Writable
    {
        protected const string indent = "    ";

        internal ModelGeneratorOptions Options { get; set; }

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
}
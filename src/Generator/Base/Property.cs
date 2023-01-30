// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal abstract class Property : Writable
{
    private string name = string.Empty;

    private string type = string.Empty;

    private bool hasBodies => Getter?.Body != null && Setter?.Body != null;

    private IDictionary<string, bool> needsConvertedMapping = new Dictionary<string, bool>
    {
        //{ "int?", false },
        //{ "int", false },
        //{ "float", false },
        //{ "float?", false },
        //{ "string", false },
        //{ "bool", false },
        //{ "bool?", false }
        { "sint32", false },
        { "float", false },
        { "double", false },
        { "string", false },
        { "bool", false },
    };

    private IList<string> interfaceTransformTypes = new List<string>
    {
        "IEnumerable",
        "IDictionary",
        "IList",
        "ICollection"
    };

    internal string Type { get => type; set => HandleTypeSetter(value); }

    internal string? NonInterfaceType { get; set; }

    internal string Name { get => name; set => name = CapitalizeFirstLetter(value); }

    internal string JsonName { get; set; } = string.Empty;

    internal string? DictionaryPatchType { get; set; }

    internal bool NeedsConvertMethod { get; set; }

    internal bool UseNonInterfaceType => NonInterfaceType != Type;

    internal bool JsonIgnore { get; set; } = false;

    internal bool Nullable { get; set; } = false;

    internal bool Initialized { get; set; } = false;

    internal bool Obsolete { get; set; } = false;

    internal PropertyGetter Getter { get; set; }

    internal PropertySetter Setter { get; set; }

    internal List<Entity> ProducedEntities { get; set; } = new List<Entity>();

    internal Property(ModelGeneratorOptions options) : base(options)
    {
        Getter = new PropertyGetter(options);
        Setter = new PropertySetter(options);
    }

    internal virtual void WriteTo(StreamWriter streamWriter, int fieldNumber)
    {
        foreach (var producedEntity in ProducedEntities)
        {
            producedEntity.GenerateFile();
        }

        //if (JsonIgnore)
        //{
        //    WriteJsonIgnoreAttribute(streamWriter);
        //}
        //else
        //{
        //    WriteJsonPropertyAttribute(streamWriter, JsonName);
        //}

        //if (Obsolete)
        //{
        //    streamWriter.WriteLine($"{indent}{indent}{Helper.ObsoleteAttribute}");
        //}

        //var nullable = Nullable ? "?" : string.Empty;
        //streamWriter.Write($"{indent}{indent}public {Type}{nullable} {Name}");
        streamWriter.WriteLine($"{indent}{Type} {Name} = {fieldNumber};");

        // If bodies exist, then add newlines to format.
        //if (hasBodies)
        //{
        //    streamWriter.WriteLine();
        //    streamWriter.WriteLine($"{indent}{indent}{{");

        //    streamWriter.Write($"{indent}{indent}{indent}");
        //    Getter.WriteTo(streamWriter);

        //    streamWriter.Write($"{indent}{indent}{indent}");
        //    Setter.WriteTo(streamWriter);

        //    streamWriter.Write($"{indent}{indent}}}");
        //}
        //else
        //{
        //    streamWriter.Write(" { ");
        //    Getter?.WriteTo(streamWriter);
        //    Setter?.WriteTo(streamWriter);
        //    streamWriter.Write("}");
        //}

        //streamWriter.WriteLine(Initialized ? $" = new {Type}();" : string.Empty);
    }

    protected void WriteJsonPropertyAttribute(StreamWriter streamWriter, string property)
    {
        streamWriter.WriteLine($"{indent}{indent}[JsonPropertyName(\"{property}\")]");
    }

    private void HandleTypeSetter(string value)
    {
        type = value;
        NonInterfaceType = interfaceTransformTypes.Any(t => type.StartsWith(t)) ? type.TrimStart('I') : type;
        NeedsConvertMethod = !needsConvertedMapping.ContainsKey(type) ? true : needsConvertedMapping[type];
        SetDictionaryPatchType();
    }

    private void SetDictionaryPatchType()
    {
        if (!type.StartsWith("IDictionary"))
        {
            return;
        }

        var end = type.Split(',')[1];
        var valueType = end.TrimStart().TrimEnd('>');
        DictionaryPatchType = $"{type}, {valueType}";
    }
}
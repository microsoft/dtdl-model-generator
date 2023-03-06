// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal abstract class Property : Writable
{
    private string name = string.Empty;

    private IDictionary<string, bool> needsConvertedMapping = new Dictionary<string, bool>
    {
        { "int?", false },
        { "int", false },
        { "float", false },
        { "float?", false },
        { "string", false },
        { "bool", false },
        { "bool?", false }
    };

    private IList<string> interfaceTransformTypes = new List<string>
    {
        "IEnumerable",
        "IDictionary",
        "IList",
        "ICollection"
    };

    internal string Type { get; set; }

    internal string Name { get => name; set => name = CapitalizeFirstLetter(value); }

    internal string JsonName { get; set; } = string.Empty;

    internal bool JsonIgnore { get; set; } = false;

    internal bool Nullable { get; } = false;

    internal bool Initialized { get; set; } = false;

    internal bool Obsolete { get; set; } = false;

    internal PropertyGetter Getter { get; set; }

    internal PropertySetter Setter { get; set; }

    internal List<Entity> ProducedEntities { get; } = new List<Entity>();

    internal Property(ModelGeneratorOptions options) : base(options)
    {
        Getter = new PropertyGetter(options);
        Setter = new PropertySetter(options);
    }

    internal virtual void WriteTo(StreamWriter streamWriter)
    {
        foreach (var producedEntity in ProducedEntities)
        {
            producedEntity.GenerateFile();
        }

        if (JsonIgnore)
        {
            WriteJsonIgnoreAttribute(streamWriter);
        }
        else
        {
            WriteJsonPropertyAttribute(streamWriter, JsonName);
        }

        if (Obsolete)
        {
            streamWriter.WriteLine($"{indent}{indent}{Helper.ObsoleteAttribute}");
        }

        var nullable = Nullable ? "?" : string.Empty;
        streamWriter.Write($"{indent}{indent}public {Type}{nullable} {Name}");
        streamWriter.Write(" { ");
        Getter?.WriteTo(streamWriter);
        Setter?.WriteTo(streamWriter);
        streamWriter.Write("}");

        streamWriter.WriteLine(Initialized ? $" = new {Type}();" : string.Empty);
    }

    protected void WriteJsonPropertyAttribute(StreamWriter streamWriter, string property)
    {
        streamWriter.WriteLine($"{indent}{indent}[JsonPropertyName(\"{property}\")]");
    }
}
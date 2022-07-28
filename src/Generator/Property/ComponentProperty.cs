// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class ComponentProperty : Property
{
    internal ComponentProperty(DTNamedEntityInfo entityInfo, DTComponentInfo componentInfo, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        var componentType = ExtractClass(componentInfo.Schema.Id);
        Type = $"{componentType?.TrimEnd('?')}?";
        Name = entityInfo.Name;
        JsonName = entityInfo.Name;
        Obsolete = entityInfo.IsObsolete();
    }

    protected static string ExtractClass(Dtmi id)
    {
        var labels = id.Labels;
        return labels[labels.Length - 1];
    }
}
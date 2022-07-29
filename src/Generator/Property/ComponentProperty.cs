// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class ComponentProperty : Property
{
    internal ComponentProperty(DTNamedEntityInfo entityInfo, DTComponentInfo componentInfo, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        var componentType = Helper.ExtractClassNameFromDtmi(componentInfo.Schema.Id);
        Type = $"{componentType?.TrimEnd('?')}?";
        DependantNamespace = Helper.ExtractNamespaceNameFromDtmi(componentInfo.Schema.Id);
        Name = entityInfo.Name;
        JsonName = entityInfo.Name;
        Obsolete = entityInfo.IsObsolete();
    }
}
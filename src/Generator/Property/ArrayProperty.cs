// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class ArrayProperty : Property
{
    internal ArrayProperty(DTNamedEntityInfo entity, ModelGeneratorOptions options) : base(options)
    {
        Name = entity.Name;
        JsonName = entity.Name;
        Obsolete = entity.IsObsolete();
        Type = "Array?";
    }
}
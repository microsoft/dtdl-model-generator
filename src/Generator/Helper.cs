// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal static class Helper
{
    internal const string ObsoleteAttribute = "[Obsolete]";

    internal static bool IsObsolete(this DTNamedEntityInfo entity)
    {
        return !string.IsNullOrEmpty(entity.Comment) && entity.Comment.Equals(ObsoleteAttribute, StringComparison.OrdinalIgnoreCase);
    }

    internal static string Serialize(this AccessModifier modifier) => modifier switch
    {
        AccessModifier.Public => "public",
        AccessModifier.Internal => "internal",
        AccessModifier.Protected => "protected",
        AccessModifier.Private => "private",
        AccessModifier.ProtectedInternal => "protected internal",
        AccessModifier.PrivateProtected => "private protected",
        _ => throw new Exception($"Invalid value for AccessModifier: {modifier}"),
    };

    internal static string ExtractClassNameFromDtmi(Dtmi id)
    {
        return id.Labels.Last();
    }

    internal static string ExtractNamespaceNameFromDtmi(Dtmi id)
    {
        return string.Join(".", id.Labels.Take(id.Labels.Length - 1)).ToLower();
    }
}
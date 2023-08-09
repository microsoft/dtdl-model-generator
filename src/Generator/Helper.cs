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

    public static int IndexOf<T>(this IReadOnlyList<T> self, T elementToFind)
    {
        int index = 0;
        foreach (T element in self)
        {
            if (Equals(element, elementToFind))
            {
                return index;
            }

            index++;
        }

        return -1;
    }
}
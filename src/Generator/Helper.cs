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
}
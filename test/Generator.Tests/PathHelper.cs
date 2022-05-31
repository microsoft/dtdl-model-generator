// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests;

internal static class PathHelper
{
    internal static string GetCombinedFullPath(params string[] paths)
    {
        if (paths != null && paths.Length == 0)
        {
            throw new ArgumentException("'paths' must not be empty", nameof(paths));
        }

        if (paths == null)
        {
            throw new ArgumentNullException(nameof(paths));
        }

        return Path.GetFullPath(Path.Combine(paths));
    }
}
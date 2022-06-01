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

    internal static string GetCombinedFullPath(string baseDir, string path, int reverseRelativeDirectories)
    {
        if (string.IsNullOrWhiteSpace(baseDir))
        {
            throw new ArgumentNullException(nameof(baseDir));
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (reverseRelativeDirectories <= 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(reverseRelativeDirectories)} must be greater than 0");
        }

        var reverseDirs = new List<string>();
        for (int i = 0; i < reverseRelativeDirectories; i++)
        {
            reverseDirs.Add("..");
        }

        var paths = new string[] { baseDir }.Concat(reverseDirs).Concat(new string[] { path }).ToArray();
        return GetCombinedFullPath(paths);
    }
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

/// <summary>
/// A class containing the options used to configure the behavior of the Generator.
/// </summary>
public class ModelGeneratorOptions
{
    /// <summary>
    /// Gets or sets the directory the DTDL json models are located at.
    /// </summary>
    public string JsonModelsDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the directory to write the generated C# classes to.
    /// </summary>
    public string OutputDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the namespace to apply to the generated classes.
    /// </summary>
    public string Namespace { get; set; } = string.Empty;
}
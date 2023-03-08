// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator.Exceptions;

/// <summary>
/// The exception that is thrown when the Generator encounters an unsupported content type in a DTDL model.
/// </summary>
public class UnsupportedContentTypeException : Exception
{
    /// <summary>
    /// The unsupported content type.
    /// </summary>
    public string ContentType { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedContentTypeException"/> class.
    /// </summary>
    /// <param name="contentType">The unsupported content type.</param>
    public UnsupportedContentTypeException(string contentType) : base($"Unsupported content type: {contentType}")
    {
        ContentType = contentType;
    }
}
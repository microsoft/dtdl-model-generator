// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator.Exceptions;

using Microsoft.Azure.DigitalTwins.Parser;

/// <summary>
/// The exception that is thrown when the Generator encounters an unsupported primative type in a DTDL model.
/// </summary>
public class UnsupportedPrimativeTypeException : Exception
{
    /// <summary>
    /// The unsupported primative type.
    /// </summary>
    public DTEntityKind PrimativeType { get; init; }

    /// <summary>
    /// The name of the entity with the unsupported primative type.
    /// </summary>
    public string EntityName { get; init; }

    /// <summary>
    /// The class that encloses the unsupported primative type.
    /// </summary>
    public string EnclosingClass { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedPrimativeTypeException"/> class.
    /// </summary>
    /// <param name="primativeType">The unsupported primative type.</param>
    /// <param name="entityName">The name of the entity with the unsupported primative type.</param>
    /// <param name="enclosingClass">The class that encloses the unsupported primative type.</param>
    public UnsupportedPrimativeTypeException(DTEntityKind primativeType, string entityName, string enclosingClass)
        : base($"Unsupported primitive property type: {primativeType} for {entityName} in {enclosingClass}")
    {
        PrimativeType = primativeType;
        EntityName = entityName;
        EnclosingClass = enclosingClass;
    }
}
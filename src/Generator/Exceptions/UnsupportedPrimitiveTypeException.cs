// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator.Exceptions;

using Microsoft.Azure.DigitalTwins.Parser;

/// <summary>
/// The exception that is thrown when the Generator encounters an unsupported primative type in a DTDL model.
/// </summary>
public class UnsupportedPrimitiveTypeException : Exception
{
    /// <summary>
    /// Gets the unsupported primative type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DTEntityKind PrimitiveType { get; init; }

    /// <summary>
    /// Gets the name of the entity with the unsupported primative type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public string EntityName { get; init; }

    /// <summary>
    /// Gets the class that encloses the unsupported primative type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public string EnclosingClass { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedPrimitiveTypeException"/> class.
    /// </summary>
    /// <param name="primitiveType">The unsupported primative type.</param>
    /// <param name="entityName">The name of the entity with the unsupported primative type.</param>
    /// <param name="enclosingClass">The class that encloses the unsupported primative type.</param>
    public UnsupportedPrimitiveTypeException(DTEntityKind primitiveType, string entityName, string enclosingClass)
        : base($"Unsupported primitive property type: {primitiveType} for {entityName} in {enclosingClass}")
    {
        PrimitiveType = primitiveType;
        EntityName = entityName;
        EnclosingClass = enclosingClass;
    }
}
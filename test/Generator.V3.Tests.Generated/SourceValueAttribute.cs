﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.V3.Tests.Generated;

using System;

/// <summary>
/// An attribute class uses to hold additional metadata available with DTDL enum properties.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class SourceValueAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the value assigned to the attribute.
    /// </summary>
    public string? Value { get; set; }
}
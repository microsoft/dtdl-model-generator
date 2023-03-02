// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal abstract class PropertyAccessor : Writable
{
    internal PropertyAccessor(ModelGeneratorOptions options) : base(options)
    {
    }
}
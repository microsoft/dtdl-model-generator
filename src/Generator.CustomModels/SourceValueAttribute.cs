//-----------------------------------------------------------------------
// <copyright file="SourceValueAttribute.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.DigitalWorkplace.Integration.Models.DigitalTwins
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SourceValueAttribute : Attribute
    {
        public string Value { get; set; }
    }
}
// -----------------------------------------------------------------------
// <copyright file="TwinEqualityComparer.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.DigitalWorkplace.Integration.Models.DigitalTwins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Azure.DigitalTwins.Core;

    internal class TwinEqualityComparer : IEqualityComparer<BasicDigitalTwin>
    {
        public bool Equals(BasicDigitalTwin x, BasicDigitalTwin y)
        {
            return
            string.Equals(x?.Metadata?.ModelId, y?.Metadata?.ModelId) &&
            string.Equals(x?.Id, y?.Id) &&
            x.Contents.Count == y.Contents.Count &&
            x.Contents.Except(y.Contents).Any();
        }

        public int GetHashCode(BasicDigitalTwin obj)
        {
#if NETSTANDARD2_1_OR_GREATER
            return HashCode.Combine(obj.Id);
#else
            return obj.Id.GetHashCode();
#endif
        }
    }
}
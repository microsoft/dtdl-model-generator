// <copyright file="RelationshipEqualityComparer.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.DigitalWorkplace.Integration.Models.DigitalTwins
{
    using Azure.DigitalTwins.Core;
    using System.Collections.Generic;

    public class RelationshipEqualityComparer : IEqualityComparer<BasicRelationship>
    {
        public bool Equals(BasicRelationship x, BasicRelationship y)
        {
            return x.Id == y.Id
                && x.Name == y.Name
                && x.SourceId == y.SourceId
                && x.TargetId == y.TargetId
                && x.Properties.DictionaryEquals(y.Properties);
        }

        public int GetHashCode(BasicRelationship obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

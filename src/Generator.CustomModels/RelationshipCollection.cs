//-----------------------------------------------------------------------
// <copyright file="RelationshipCollection.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.DigitalWorkplace.Integration.Models.DigitalTwins
{
    using Azure.DigitalTwins.Core;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class RelationshipCollection<TRelationship, TTarget> : BasicRelationship, IEnumerable<TRelationship>
        where TRelationship : Relationship<TTarget>, new()
        where TTarget : BasicDigitalTwin
    {
        private readonly ICollection<TRelationship> relationships = new List<TRelationship>();

        public RelationshipCollection()
        {
            Name = new TRelationship().Name;
        }

        public RelationshipCollection(IEnumerable<TRelationship> relationships) : this()
        {
            this.relationships = new List<TRelationship>(relationships);
        }

        /// <summary>
        /// Gets the target twins of this relationship collection.
        /// </summary>
        public IEnumerable<TTarget> Targets => relationships.Select(r => r.Target);

        public IEnumerator<TRelationship> GetEnumerator() => relationships.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => relationships.GetEnumerator();
    }
}

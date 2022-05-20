// <copyright file="Relationship.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.DigitalWorkplace.Integration.Models.DigitalTwins
{
    using Azure.DigitalTwins.Core;
    using System;
    using System.Text.Json.Serialization;

    public abstract class Relationship<TTarget> : BasicRelationship, IEquatable<BasicRelationship>
        where TTarget : BasicDigitalTwin
    {
        [JsonIgnore]
        public TTarget Target { get; set; }

        public abstract bool Equals(BasicRelationship other);

        protected void InitializeFromTwins(BasicDigitalTwin source, TTarget target)
        {
            Id = $"{source.Id}-{Name}->{target.Id}";
            SourceId = source.Id;
            TargetId = target.Id;
            Target = target;
        }
    }
}
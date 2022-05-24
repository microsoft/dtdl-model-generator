// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Generator.Tests.Generated
{
    using Azure;
    using Azure.DigitalTwins.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [Serializable]
    public class Media : IEquatable<Media>
    {

        private const string isVisible = nameof(isVisible);
        private const string caption = nameof(caption);
        private const string mediaUrl = nameof(mediaUrl);

        [JsonPropertyName(isVisible)]
        public bool? IsVisible { get; set; }
        [JsonPropertyName(caption)]
        public string? Caption { get; set; }
        [JsonPropertyName(mediaUrl)]
        public string? MediaUrl { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Media);
        }

        public bool Equals(Media? other)
        {
            return !(other is null) && IsVisible == other.IsVisible && Caption == other.Caption && MediaUrl == other.MediaUrl;
        }

        public static bool operator ==(Media left, Media right)
        {
            return EqualityComparer<Media>.Default.Equals(left, right);
        }

        public static bool operator !=(Media left, Media right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(IsVisible?.GetHashCode(), Caption?.GetHashCode(), MediaUrl?.GetHashCode());
        }
    }
}
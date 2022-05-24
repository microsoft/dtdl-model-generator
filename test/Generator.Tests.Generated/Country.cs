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

    public class Country : BasicDigitalTwin, IEquatable<Country>, IEquatable<BasicDigitalTwin>
    {
        public Country()
        {
            Metadata.ModelId = ModelId;
        }
        [JsonIgnore]
        public static string ModelId { get; } = "dtmi:test:Country;1";

        private const string number = nameof(number);
        private const string name = nameof(name);
        private const string shortName = nameof(shortName);
        private const string code = nameof(code);
        private const string shortCode = nameof(shortCode);
        private const string officialCountryShortName = nameof(officialCountryShortName);
        private const string officialCountryLongName = nameof(officialCountryLongName);
        private const string postalCodeLengthQuantity = nameof(postalCodeLengthQuantity);
        private const string postalCodeMaskDescription = nameof(postalCodeMaskDescription);
        private const string postalCodeMaskExpression = nameof(postalCodeMaskExpression);
        private const string unitOfMeasure = nameof(unitOfMeasure);

        [JsonPropertyName(number)]
        public string? Number { get; set; }
        [JsonPropertyName(name)]
        public string? Name { get; set; }
        [JsonPropertyName(shortName)]
        public string? ShortName { get; set; }
        [JsonPropertyName(code)]
        public string? Code { get; set; }
        [JsonPropertyName(shortCode)]
        public string? ShortCode { get; set; }
        [JsonPropertyName(officialCountryShortName)]
        public string? OfficialCountryShortName { get; set; }
        [JsonPropertyName(officialCountryLongName)]
        public string? OfficialCountryLongName { get; set; }
        [JsonPropertyName(postalCodeLengthQuantity)]
        public string? PostalCodeLengthQuantity { get; set; }
        [JsonPropertyName(postalCodeMaskDescription)]
        public string? PostalCodeMaskDescription { get; set; }
        [JsonPropertyName(postalCodeMaskExpression)]
        public string? PostalCodeMaskExpression { get; set; }
        [JsonPropertyName(unitOfMeasure)]
        public string? UnitOfMeasure { get; set; }
        [JsonIgnore]
        public CountryHasStateRelationshipCollection HasState { get; set; } = new CountryHasStateRelationshipCollection();
        [JsonIgnore]
        public CountryHasCityRelationshipCollection HasCity { get; set; } = new CountryHasCityRelationshipCollection();
        public override bool Equals(object? obj)
        {
            return Equals(obj as Country);
        }

        public bool Equals(Country? other)
        {
            return !(other is null) && Id == other.Id && Metadata.ModelId == other.Metadata.ModelId && Number == other.Number && Name == other.Name && ShortName == other.ShortName && Code == other.Code && ShortCode == other.ShortCode && OfficialCountryShortName == other.OfficialCountryShortName && OfficialCountryLongName == other.OfficialCountryLongName && PostalCodeLengthQuantity == other.PostalCodeLengthQuantity && PostalCodeMaskDescription == other.PostalCodeMaskDescription && PostalCodeMaskExpression == other.PostalCodeMaskExpression && UnitOfMeasure == other.UnitOfMeasure;
        }

        public static bool operator ==(Country left, Country right)
        {
            return EqualityComparer<Country>.Default.Equals(left, right);
        }

        public static bool operator !=(Country left, Country right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.CustomHash(Id?.GetHashCode(), Metadata?.ModelId?.GetHashCode(), Number?.GetHashCode(), Name?.GetHashCode(), ShortName?.GetHashCode(), Code?.GetHashCode(), ShortCode?.GetHashCode(), OfficialCountryShortName?.GetHashCode(), OfficialCountryLongName?.GetHashCode(), PostalCodeLengthQuantity?.GetHashCode(), PostalCodeMaskDescription?.GetHashCode(), PostalCodeMaskExpression?.GetHashCode(), UnitOfMeasure?.GetHashCode());
        }

        public bool Equals(BasicDigitalTwin? other)
        {
            return Equals(other as Country) || new TwinEqualityComparer().Equals(this, other);
        }
    }
}
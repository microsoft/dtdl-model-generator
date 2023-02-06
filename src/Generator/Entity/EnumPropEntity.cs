// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Azure.DigitalTwins.Parser;

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal class EnumPropEntity : EnumEntity
{
    internal DTEnumInfo EnumInfo { get; set; }

    internal EnumPropEntity(DTEnumInfo enumInfo, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        EnumInfo = enumInfo;
        if (enumInfo.Id.AbsoluteUri.Contains("__"))
        {
            var prop = enumInfo.Id.AbsoluteUri.Split("__").Last().Split(":").First();
            Name = enclosingClass + char.ToUpper(prop[0]) + prop.Substring(1);
        }
        else
        {
            var dtmiNameSpace = enumInfo.Id.AbsoluteUri.Split(';')[0];
            Name = dtmiNameSpace.Split(':').Last();
        }

        AllowOverwrite = true;
    }

    protected override void WriteUsingStatements(StreamWriter streamWriter)
    {
        //WriteUsingDataAnnotation(streamWriter);
        //base.WriteUsingStatements(streamWriter);
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        var index = 0;
        foreach (var enumValue in EnumInfo.EnumValues)
        {
            const string en = nameof(en);
            var hasEnDescription = enumValue.Description.ContainsKey(en) && !string.IsNullOrEmpty(enumValue.Description[en]);
            var enDescription = hasEnDescription ? $"{enumValue.Description[en]}" : string.Empty;
            if (!string.IsNullOrEmpty(enDescription))
            {
                streamWriter.WriteLine($"{indent}// {enDescription}");
            }

            var currentEnumValue = ConvertEnumValueToProtobufNamingConvention($"{Name}{enumValue.EnumValue}");
            if (!string.IsNullOrEmpty(enumValue.Comment) && int.TryParse(enumValue.Comment, out int enumIndex))
            {
                streamWriter.WriteLine($"{indent}{currentEnumValue} = {enumIndex}{";"}");
            }
            else
            {
                streamWriter.WriteLine($"{indent}{currentEnumValue} = {index}{";"}");
                index++;
            }
        }
    }

    private void WriteEnumAttributes(StreamWriter streamWriter, DTEnumValueInfo enumValueInfo)
    {
        const string en = nameof(en);
        var enumMember = $"EnumMember(Value = \"{enumValueInfo.EnumValue}\")";
        var hasEnDescription = enumValueInfo.Description.ContainsKey(en) && !string.IsNullOrEmpty(enumValueInfo.Description[en]);
        var enDescription = hasEnDescription ? $", Description = \"{enumValueInfo.Description[en]}\"" : string.Empty;
        var display = $"Display(Name = \"{enumValueInfo.DisplayName[en]}\"{enDescription})";
        var sourceValue = string.IsNullOrEmpty(enumValueInfo.Comment) ? string.Empty : $"SourceValue(Value = \"{enumValueInfo.Comment}\")";
        if (!string.IsNullOrEmpty(sourceValue))
        {
            streamWriter.WriteLine($"{indent}{indent}[{string.Join(", ", enumMember, display, sourceValue)}]");
        }
        else
        {
            streamWriter.WriteLine($"{indent}{indent}[{string.Join(", ", enumMember, display)}]");
        }
    }
}
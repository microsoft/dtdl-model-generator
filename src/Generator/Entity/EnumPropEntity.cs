﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

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
        WriteUsingDataAnnotation(streamWriter);
        base.WriteUsingStatements(streamWriter);
    }

    protected override void WriteContent(StreamWriter streamWriter)
    {
        foreach (var enumValue in EnumInfo.EnumValues)
        {
            var isLastItem = EnumInfo.EnumValues.IndexOf(enumValue) == EnumInfo.EnumValues.Count - 1;
            WriteEnumAttributes(streamWriter, enumValue);
            var comma = isLastItem ? string.Empty : ",";
            streamWriter.WriteLine($"{indent}{indent}{enumValue.EnumValue}{comma}");
        }
    }

    private void WriteEnumAttributes(StreamWriter streamWriter, DTEnumValueInfo enumValueInfo)
    {
        const string en = nameof(en);
        var enumMember = $"EnumMember(Value = \"{enumValueInfo.EnumValue}\")";
        var hasEnDescription = enumValueInfo.Description.ContainsKey(en) && !string.IsNullOrEmpty(enumValueInfo.Description[en]);
        var enDescription = hasEnDescription ? $", Description = \"{enumValueInfo.Description[en]}\"" : string.Empty;
        var hasDisplayName = enumValueInfo.DisplayName.ContainsKey(en) && !string.IsNullOrEmpty(enumValueInfo.DisplayName[en]);
        var displayName = hasDisplayName ? enumValueInfo.DisplayName[en] : enumValueInfo.Name;
        var display = $"Display(Name = \"{displayName}\"{enDescription})";
        var hasSourceValue = enumValueInfo.Comment ?? enumValueInfo.EnumValue;
        var sourceValue = string.IsNullOrEmpty(hasSourceValue.ToString()) ? string.Empty : $"SourceValue(Value = \"{hasSourceValue}\")";

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

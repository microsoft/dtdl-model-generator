// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.Integration.Models.Generator;

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
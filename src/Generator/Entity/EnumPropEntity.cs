// Copyright (c) Microsoft Corporation.
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
            var uriSpli = enumInfo.Id.AbsoluteUri.Split("__");
            var prop = uriSpli.Last().Split(":").First();
            Name = enclosingClass + char.ToUpper(prop[0]) + prop.Substring(1);

            var namespaceSplit = uriSpli.First().Split(enclosingClass).First().Split(':');
            FileDirectory = Path.Combine(namespaceSplit.Take(namespaceSplit.Length - 1).TakeLast(namespaceSplit.Length - 2).ToArray()).ToLower();
        }
        else
        {
            var dtmiNameSpace = enumInfo.Id.AbsoluteUri.Split(';')[0];
            var dtmiNameSpaceSplit = dtmiNameSpace.Split(':');
            var dtmiNameSpaceSplitCount = dtmiNameSpaceSplit.Count();
            Name = dtmiNameSpaceSplit.Last();

            FileDirectory = Path.Combine(dtmiNameSpaceSplit.Take(dtmiNameSpaceSplitCount - 1).TakeLast(dtmiNameSpaceSplitCount - 2).ToArray()).ToLower();
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
        var hasEnDisplay = enumValueInfo.DisplayName.ContainsKey(en) && !string.IsNullOrEmpty(enumValueInfo.DisplayName[en]);
        var display = $"Display(Name = \"{enumValueInfo.EnumValue}\")";
        if (hasEnDisplay)
        {
            display = $"Display(Name = \"{enumValueInfo.DisplayName[en]}\")";
            if (hasEnDescription)
            {
                var enDescription = $", Description = \"{enumValueInfo.Description[en]}\"";
                display = $"Display(Name = \"{enumValueInfo.DisplayName[en]}\"{enDescription})";
            }
        }

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
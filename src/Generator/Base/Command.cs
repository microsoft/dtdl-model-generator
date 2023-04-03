// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

using Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator.Exceptions;

internal class Command : Writable
{
    private DTCommandInfo CommandInfo { get; init; }

    private string EnclosingClass { get; init; }

    public List<Entity> ProducedEntities { get; set; } = new List<Entity>();

    internal Command(DTCommandInfo commandInfo, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        CommandInfo = commandInfo;
        EnclosingClass = enclosingClass;
    }

    internal virtual void WriteTo(StreamWriter streamWriter)
    {
        var requestType = GetDTCommandPayloadType(CommandInfo, CommandInfo.Request, EnclosingClass, Options, "Request");
        var responseType = GetDTCommandPayloadType(CommandInfo, CommandInfo.Response, EnclosingClass, Options, "Response");

        foreach (var producedEntity in ProducedEntities)
        {
            producedEntity.GenerateFile();
        }

        // with moduleId argument
        WriteCommandMethodTo(streamWriter, true, requestType, responseType);

        // with moduleId argument
        WriteCommandMethodTo(streamWriter, false, requestType, responseType);
    }

    private void WriteCommandMethodTo(StreamWriter streamWriter, bool includeModuleId, string requestType, string responseType)
    {
        var commandName = CommandInfo.Name;
        var responseName = CommandInfo.Response is null ? string.Empty : CommandInfo.Response.Name;
        var resultType = CommandInfo.Response is null ? "int" : $"(int status, {responseType} {responseName})";
        var requestName = CommandInfo.Request is null ? string.Empty : CommandInfo.Request.Name;
        var requestMethodArgumentDeclaration = CommandInfo.Request is null ? string.Empty : $"{requestType} {requestName}, ";
        var requestMethodArgumentPassed = CommandInfo.Request is null ? string.Empty : $"{requestName}, ";
        var commandFunctionRequestResponseType = GetTypeParameters(CommandInfo, requestType, responseType);
        var moduleIdMethodArgumentDeclaration = includeModuleId ? "string moduleId, " : string.Empty;
        var moduleIdMethodArgumentPassed = includeModuleId ? "moduleId, " : "null, ";

        streamWriter.WriteLine($"{indent}{indent}public static async Task<{resultType}> {CapitalizeFirstLetter(commandName)}Async(ServiceClient serviceClient, string deviceId, {moduleIdMethodArgumentDeclaration}{requestMethodArgumentDeclaration}CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return await CommandHelper.SendCommandAsync{commandFunctionRequestResponseType}(serviceClient, deviceId, {moduleIdMethodArgumentPassed}\"{commandName}\", {requestMethodArgumentPassed}options, cancellationToken).ConfigureAwait(false);");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }

    private string GetTypeParameters(DTCommandInfo commandInfo, string requestType, string responseType)
    {
        if (commandInfo.Request is null && commandInfo.Response is null)
        {
            return string.Empty;
        }
        else if (commandInfo.Request is null)
        {
            return $"<{responseType}>";
        }
        else if (commandInfo.Response is null)
        {
            return $"<{requestType}>";
        }
        else
        {
            return $"<{requestType}, {responseType}>";
        }
    }

    private string GetDTCommandPayloadType(DTCommandInfo commandInfo, DTCommandPayloadInfo? commandPayloadInfo, string enclosingClass, ModelGeneratorOptions options, string commandPayloadTypeSuffix)
    {
        if (commandPayloadInfo is null)
        {
            return string.Empty;
        }

        switch (commandPayloadInfo.Schema)
        {
            case DTMapInfo mapInfo:
                var mapProperty = new MapProperty(commandPayloadInfo, mapInfo, commandPayloadInfo.Name, options);
                return mapProperty.Type;

            case DTEnumInfo enumInfo:
                var commandPayloadType = $"{enclosingClass}{CapitalizeFirstLetter(CommandInfo.Name)}{commandPayloadTypeSuffix}";
                var enumProperty = new EnumProperty(commandPayloadInfo, enumInfo, commandPayloadInfo.Name, options);
                enumProperty.Name = commandPayloadType;
                enumProperty.Type = $"{enumProperty.Name}?";
                var enumEntity = new EnumPropEntity(enumInfo, commandPayloadInfo.Name, options);
                enumEntity.Name = commandPayloadType;
                ProducedEntities.Add(enumEntity);
                return enumProperty.Type;

            case DTObjectInfo objectInfo:
                commandPayloadType = $"{enclosingClass}{CapitalizeFirstLetter(CommandInfo.Name)}{commandPayloadTypeSuffix}";
                var objectEntity = new ObjectEntity(commandInfo, objectInfo, enclosingClass, options);
                objectEntity.Name = commandPayloadType;
                ProducedEntities.Add(objectEntity);
                return commandPayloadType;

            case DTDurationInfo:
                var durationProperty = new DurationProperty(commandPayloadInfo, Options);
                return durationProperty.Type;

            case DTDateInfo:
                var dateOnlyProperty = new DateOnlyProperty(commandPayloadInfo, Options);
                return dateOnlyProperty.Type;

            default:
                if (!Types.TryGetNullable(commandPayloadInfo.Schema.EntityKind, out var type) || type is null)
                {
                    throw new UnsupportedPrimativeTypeException(commandPayloadInfo.Schema.EntityKind, commandPayloadInfo.Name, enclosingClass);
                }

                return type;
        }
    }
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

using Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator.Exceptions;

internal class Command : Writable
{
    public List<Entity> ProducedEntities { get; set; } = new List<Entity>();

    private string commandName { get; set; }

    private string requestType { get; set; }

    private string responseType { get; set; }

    private string requestName { get; set; }

    private string responseName { get; set; }

    private string resultType { get; set; }

    private string requestMethodArgumentDeclaration { get; set; }

    private string requestMethodArgumentName { get; set; }

    private string commandFunctionRequestResponseType { get; set; }

    internal Command(DTCommandInfo commandInfo, string enclosingClass, ModelGeneratorOptions options) : base(options)
    {
        commandName = commandInfo.Name;
        requestType = GetDTCommandPayloadType(commandInfo, commandInfo.Request, commandInfo.Name, enclosingClass, options, "Request");
        responseType = GetDTCommandPayloadType(commandInfo, commandInfo.Response, commandInfo.Name, enclosingClass, options, "Response");
        responseName = commandInfo.Response is null ? string.Empty : commandInfo.Response.Name;
        resultType = commandInfo.Response is null ? "int" : $"(int status, {responseType} {responseName})";
        requestName = commandInfo.Request is null ? string.Empty : commandInfo.Request.Name;
        requestMethodArgumentDeclaration = commandInfo.Request is null ? string.Empty : $"{requestType} {requestName}, ";
        requestMethodArgumentName = commandInfo.Request is null ? string.Empty : $"{requestName}, ";
        commandFunctionRequestResponseType = GetTypeParameters(commandInfo, requestType, responseType);
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

    private string GetDTCommandPayloadType(DTCommandInfo commandInfo, DTCommandPayloadInfo? commandPayloadInfo, string commandName, string enclosingClass, ModelGeneratorOptions options, string commandPayloadTypeSuffix)
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
                var commandPayloadType = $"{enclosingClass}{CapitalizeFirstLetter(commandName)}{commandPayloadTypeSuffix}";
                var enumProperty = new EnumProperty(commandPayloadInfo, enumInfo, commandPayloadInfo.Name, options);
                enumProperty.Name = commandPayloadType;
                enumProperty.Type = $"{enumProperty.Name}?";
                var enumEntity = new EnumPropEntity(enumInfo, commandPayloadInfo.Name, options);
                enumEntity.Name = commandPayloadType;
                ProducedEntities.Add(enumEntity);
                return enumProperty.Type;

            case DTObjectInfo objectInfo:
                commandPayloadType = $"{enclosingClass}{CapitalizeFirstLetter(commandName)}{commandPayloadTypeSuffix}";
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

    internal virtual void WriteTo(StreamWriter streamWriter)
    {
        foreach (var producedEntity in ProducedEntities)
        {
            producedEntity.GenerateFile();
        }

        // with moduleId argument
        streamWriter.WriteLine($"{indent}{indent}public static async Task<{resultType}> {CapitalizeFirstLetter(commandName)}Async(ServiceClient serviceClient, string deviceId, {requestMethodArgumentDeclaration}CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return await CommandHelper.SendCommandAsync{commandFunctionRequestResponseType}(serviceClient, deviceId, null, \"{commandName}\", {requestMethodArgumentName}options, cancellationToken).ConfigureAwait(false);");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();

        // without moduleId argument
        streamWriter.WriteLine($"{indent}{indent}public static async Task<{resultType}> {CapitalizeFirstLetter(commandName)}Async(ServiceClient serviceClient, string deviceId, string moduleId, {requestMethodArgumentDeclaration}CloudToDeviceMethodOptions? options = null, CancellationToken cancellationToken = default)");
        streamWriter.WriteLine($"{indent}{indent}{{");
        streamWriter.WriteLine($"{indent}{indent}{indent}return await CommandHelper.SendCommandAsync{commandFunctionRequestResponseType}(serviceClient, deviceId, moduleId, \"{commandName}\", {requestMethodArgumentName}options, cancellationToken).ConfigureAwait(false);");
        streamWriter.WriteLine($"{indent}{indent}}}");
        streamWriter.WriteLine();
    }
}
﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

internal abstract class Entity : Writable
{
    private string name = string.Empty;

    internal string FileName => $"{Name}.cs";

    internal string Name { get => name; set => name = CapitalizeFirstLetter(value); }

    internal string? FileDirectory { get; set; }

    protected bool AllowOverwrite { get; set; } = true;

    protected Entity(ModelGeneratorOptions options) : base(options)
    {
    }

    protected abstract void WriteSignature(StreamWriter streamWriter);

    protected abstract void WriteContent(StreamWriter streamWriter);

    protected static string ExtractDirectory(Dtmi id)
    {
        var labels = id.Labels;
        var count = labels.Length;
        return Path.Combine(labels.Take(count - 1).TakeLast(count - 2).ToArray());
    }

    internal virtual void GenerateFile()
    {
        using var streamWriter = CreateStreamWriter();
        WriteFile(streamWriter);
    }

    protected virtual void WriteHeader(StreamWriter streamWriter)
    {
        if (string.IsNullOrEmpty(Options.CopyrightHeader))
        {
            return;
        }

        streamWriter.WriteLine(Options.CopyrightHeader);
        streamWriter.WriteLine();
    }

    protected virtual void WriteNamespace(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"namespace {Options?.Namespace}");
    }

    protected virtual void WriteUsingStatements(StreamWriter streamWriter)
    {
        WriteUsingSerialization(streamWriter);
    }

    protected void WriteUsingDataAnnotation(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System.ComponentModel.DataAnnotations;");
    }

    protected void WriteUsingCollection(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System.Collections.Generic;");
    }

    protected void WriteUsingAdt(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using Azure.DigitalTwins.Core;");
    }

    protected void WriteUsingSystem(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System;");
    }

    protected void WriteUsingSystemRuntime(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System.Runtime;");
    }

    protected void WriteUsingSerialization(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System.Runtime.Serialization;");
        streamWriter.WriteLine($"{indent}using System.Text.Json.Serialization;");
    }

    protected void WriteStringEnumConverterAttribute(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}[JsonConverter(typeof(JsonStringEnumConverter))]");
    }

    protected void WriteUsingAzure(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using Azure;");
    }

    protected void WriteUsingReflection(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System.Reflection;");
    }

    protected void WriteUsingLinq(StreamWriter streamWriter)
    {
        streamWriter.WriteLine($"{indent}using System.Linq;");
    }

    private StreamWriter CreateStreamWriter()
    {
        var filePath = FileName;
        if (!string.IsNullOrEmpty(FileDirectory))
        {
            Directory.CreateDirectory(GetAbsolutePath(FileDirectory));
            filePath = Path.Combine(FileDirectory, FileName);
        }

        var fileAbsolutePath = GetAbsolutePath(filePath);
        var writeMode = AllowOverwrite ? FileMode.Create : FileMode.CreateNew;
        var fileStream = new FileStream(fileAbsolutePath, writeMode);
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"Generating {fileAbsolutePath}");
#endif
        return new StreamWriter(fileStream);
    }

    private string GetAbsolutePath(string directory)
    {
        return Path.Combine(Options?.OutputDirectory ?? string.Empty, directory);
    }

    private void WriteFile(StreamWriter streamWriter)
    {
        WriteHeader(streamWriter);
        WriteNamespace(streamWriter);
        streamWriter.WriteLine("{");
        WriteEntity(streamWriter);
        streamWriter.Write("}");
    }

    private void WriteEntity(StreamWriter streamWriter)
    {
        WriteUsingStatements(streamWriter);
        streamWriter.WriteLine();
        WriteSignature(streamWriter);
        streamWriter.WriteLine($"{indent}{{");
        WriteContent(streamWriter);
        streamWriter.WriteLine($"{indent}}}");
    }
}

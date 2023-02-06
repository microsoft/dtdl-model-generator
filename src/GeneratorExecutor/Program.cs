using Microsoft.DigitalWorkplace.DigitalTwins.Models.Generator;

var options = new ModelGeneratorOptions
{
    OutputDirectory = "C:\\code\\GitHub\\dtdl-model-generator\\src\\GeneratorExecutor\\Output",
    Namespace = "microsoft.outlook.services.scheduling.places.api.v2",
    JsonModelsDirectory = "C:\\code\\GitHub\\dtdl-model-generator\\src\\GeneratorExecutor\\Input",
    CopyrightHeader = "// Copyright (c) Microsoft Corporation.\n// Licensed under the MIT License."
};
var generator = new ModelGenerator(options);
await generator.GenerateClassesAsync().ConfigureAwait(false);
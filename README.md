# DTDL Model Generator

[![Build](https://github.com/microsoft/dtdl-model-generator/actions/workflows/build.yml/badge.svg)](https://github.com/microsoft/dtdl-model-generator/actions/workflows/build.yml) [![Release](https://github.com/microsoft/dtdl-model-generator/actions/workflows/release.yml/badge.svg)](https://github.com/microsoft/dtdl-model-generator/actions/workflows/release.yml)

This Digital Twin Definition Language (DTDL) Model Generator parses your DTDL json files and generates C# POCO classes to be used when interacting with the Azure Digital Twins SDK. This is made possible because all of the generated model classes inherit from the ADT-provided BasicDigitalTwin class.

## Project Components

- *Generator*: This is the core aspect of this project and is the package that's published to NuGet.
- *Generator.TemplateProject*: This is a template project that serves a couple purposes.
    1. It serves as a holding-ground for our custom, complementary classes that help connect the dots between certain aspects of the generated model classes.
    2. In the event a user of the generator doesn't have their own project destination to place the generated classes, this project serves as a template for the user to start from. Our generator will inject the correct Namespace and Assembly information into the template project based on options passed into the Generator.
- *Generator.Tests*: This is the test project that we use to test our generator.
- *Generator.Tests.Generated*: This is an ouput project from our Generated that is produced when we run unit tests for our generator. Having this here also allows us to run unit tests against the generated models to ensure various functionality (like our custom equality implementations) are working as expected.

## ModelGeneratorOptions

- *Namespace*: The namespace that will be injected into the generated model classes.
- *JsonModelsDirectory*: The directory that contains the DTDL json files that we'll be parsing.
- *OutputDirectory*: The directory that the generated model classes will be placed in.
- *IncludeTemplateProject*: Whether or not to include the template project in the output directory.

## Usage

``` csharp
using Microsoft.DigitalWorkplace.Integration.Models.Generator;

//...

var jsonDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDtdlModels");
var options = new ModelGeneratorOptions
{
    OutputDirectory = "..\\..\\..\\..\\Generator.Tests.Generated",
    IncludeTemplateProject = true,
    Namespace = "Generator.Tests.Generated",
    JsonModelsDirectory = jsonDir
};

var generator = new ModelGenerator(options);
await generator.GenerateClassesAsync();
```

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Security

For guidance on reporing security issues, please refer to the [security](SECURITY.md) section.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.

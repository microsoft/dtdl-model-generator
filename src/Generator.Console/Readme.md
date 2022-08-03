# DTDL Model Generator Console

This tools is designed to make it easy to parse your DTDL json files and generate C# POCO classes to be used when interacting with the Azure Digital Twins SDK. 

## Usage

`Generator.Console [options] <JsonModelsDirectory> <OutputDirectory>`

## Options

Generator.Console
- `--Namespace <Namespace>`: The namespace that will be injected into the generated model classes.
- `--CopyrightHeader <CopyrightHeader>`: The copyright header to include in the generated classes.

## Arguments

- JsonModelsDirectory: The directory that contains the DTDL json files that we'll be parsing.
- OutputDirectory: The directory that the generated model classes will be placed in.

## Example usage

- Generate the opendigitaltwins-building model: `Generator.Console.exe --Namespace "opendigitaltwins.building" ".\ontology_20220524" ".\generatedmodels_20220524"`

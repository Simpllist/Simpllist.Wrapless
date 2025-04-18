using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Simpllist.Attributes;
using System.Collections.Generic;

namespace Simpllist.Generators;

[Generator(LanguageNames.CSharp)]
public class AttributeGenerator : IIncrementalGenerator
{

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(PostInitializationCallback);
    }

    private static void PostInitializationCallback(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(ModuleAssemblyAttribute.FileName, ModuleAssemblyAttribute.Value);
        context.AddSource(InputAttribute.FileName, InputAttribute.Value);
    }
}
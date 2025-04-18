using Microsoft.CodeAnalysis;
using Simpllist.Attributes;
using Simpllist.Interfaces;

namespace Simpllist.Generators;

[Generator(LanguageNames.CSharp)]
public class InterfaceGenerator : IIncrementalGenerator
{

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(PostInitializationCallback);
    }

    private static void PostInitializationCallback(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(ModuleInterface.FileName, ModuleInterface.Value);
    }
}
using Microsoft.CodeAnalysis;
using Simpllist.Attributes;

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
        context.AddSource(LibraryAssemblyAttribute.FileName, LibraryAssemblyAttribute.Value);
        context.AddSource(InputAttribute.FileName, InputAttribute.Value);
    }
}
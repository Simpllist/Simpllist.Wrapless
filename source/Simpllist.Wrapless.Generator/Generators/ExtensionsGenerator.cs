using Microsoft.CodeAnalysis;
using Simpllist.Extensions;

namespace Simpllist.Generators;

[Generator(LanguageNames.CSharp)]
public class ExtensionsGenerator : IIncrementalGenerator
{

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(PostInitializationCallback);
    }

    private static void PostInitializationCallback(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(ConversionExtensions.FileName, ConversionExtensions.Value);
    }
}
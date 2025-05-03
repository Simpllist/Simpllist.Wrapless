using Microsoft.CodeAnalysis;
using Simpllist.Delegates;

namespace Simpllist.Generators;

[Generator(LanguageNames.CSharp)]
public class DelegateGenerator : IIncrementalGenerator
{

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(PostInitializationCallback);
    }

    private static void PostInitializationCallback(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(OutputDelegates.FileName, OutputDelegates.Value);
    }
}
using System.Runtime.CompilerServices;

namespace Simpllist.Services;

/// <summary>
/// Compiles all .usp file in a directory
/// </summary>
public sealed class SimplPlusDirectoryCompiler
{
    private readonly SimplPlusFileCompiler _compiler;

    public SimplPlusDirectoryCompiler(SimplPlusFileCompiler compiler)
    {
        _compiler = compiler;
    }

    /// <summary>
    /// Compiles each .usp file in the directory 1 by 1
    /// </summary>
    /// <param name="directory">The directory containing .usp files.</param>
    /// <param name="token">A cancellation token to stop the compilations.</param>
    /// <returns>An async iterator containing the resulting usp file information and exit code</returns>
    public async IAsyncEnumerable<(FileInfo? File, int ExitCode)> CompileSimplPlus(DirectoryInfo directory, [EnumeratorCancellation] CancellationToken token)
    {
        var modules = directory.GetFiles(".usp");

        foreach (var fileInfo in modules)
        {
            var output = await _compiler.CompileSimplPlus(fileInfo, token);
            yield return output;
        }
    }
}
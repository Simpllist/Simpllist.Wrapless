using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Simpllist.Services;

/// <summary>
/// Compiles a single SIMPL+ file
/// </summary>
public sealed class SimplPlusFileCompiler 
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Creates a new instance of the single file compiler.
    /// </summary>
    /// <param name="serviceProvider">The service provider</param>
    public SimplPlusFileCompiler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Compiles the provided file
    /// </summary>
    /// <param name="file">A .usp file.</param>
    /// <param name="token">cancellation token to stop the compilation</param>
    /// <returns></returns>
    public async Task<(FileInfo? File, int ExitCode)> CompileSimplPlus(FileInfo file, CancellationToken token)
    {
        using var compiler = _serviceProvider
            .GetRequiredService<Func<string, SimplPlusCompiler>>()
            .Invoke(file.FullName);

        var exitCode = await compiler.CompileSimplPlusModule(token);

        if (exitCode != 0)
        {
            return (null, exitCode);
        }

        var ush = new FileInfo(file.FullName.Replace(".usp", ".ush"));

        Debug.Assert(ush.Exists, "The output should have created a .ush file");

        return (ush, exitCode);
    }
}
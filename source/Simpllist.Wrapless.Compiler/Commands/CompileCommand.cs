using System.Diagnostics.CodeAnalysis;
using Cocona;
using Microsoft.Extensions.Logging;
using Simpllist.Services;
using System.IO;

namespace Simpllist.Commands;

/// <summary>
/// A CLI command to invoke the SIMPL+ compiler and build the .ush file from the generated usp.
/// </summary>
public sealed class CompileCommand
{
    private readonly ILogger<CompileCommand> _logger;
    private readonly CancellationToken _stoppingToken;
    private readonly SimplPlusFileCompiler _fileCompiler;
    private readonly SimplPlusDirectoryCompiler _directoryCompiler;

    /// <summary>
    /// Creates the new compile command
    /// </summary>
    /// <param name="logger">A logger for console output</param>
    /// <param name="cts">cancellation token to force exit the process</param>
    /// <param name="fileCompiler">compiles a single usp file</param>
    /// <param name="directoryCompiler">compiles a directory of usp files.</param>
    public CompileCommand(ILogger<CompileCommand> logger, CancellationTokenSource cts, SimplPlusFileCompiler fileCompiler, SimplPlusDirectoryCompiler directoryCompiler)
    {
        _logger = logger;
        _stoppingToken = cts.Token;
        _fileCompiler = fileCompiler;
        _directoryCompiler = directoryCompiler;
    }

    /// <summary>
    /// A CLI command to invoke the SIMPL+ compiler and build the .ush file from the generated usp.
    /// </summary>
    /// <param name="path">The path to the .usp file</param>
    /// <param name="destination">An optional destination to copy the copied file to.</param>
    /// <returns>An async task compiling the .usp file provided.</returns>
    [Command("compile", Aliases = new string[] { "c" }, Description = "Complies the generated Simpl Plus Wrapper")]
    public async Task CreateDriver(
        [Option(
            shortName: 'p',
            Description = "the usp file path",
            ValueName = "path")] string path,
        [Option(
            shortName: 'o',
            Description = "the output path for the compiled SIMPL+ wrapper",
            ValueName = "output")] string? destination)
    {
        var info = new FileInfo(path);

        if (destination is not null && !Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        if (info.Attributes.HasFlag(FileAttributes.Directory))
        {
            await CompileDirectory(destination, info);
            return;
        }
        
        await CompileFile(destination, info);
    }

    private async Task CompileFile(string? destination, FileInfo info)
    {
        _logger.LogInformation("Compiling SIMPL Plus USP {path}", info);

        var (ush, exitCode) = await _fileCompiler.CompileSimplPlus(info, _stoppingToken);

        if (exitCode != 0)
        {
            Environment.Exit(exitCode);
        }

        if (destination is null)
        {
            Environment.Exit(exitCode);
            return;
        }

        File.Copy(ush!.FullName, destination);
        Environment.Exit(exitCode);
    }

    private async Task CompileDirectory(string? destination, FileInfo info)
    {
        _logger.LogInformation("Compiling all SIMPL Plus USP File in {path}", info);

        await foreach (var (compiledUsh, code) in _directoryCompiler.CompileSimplPlus(info.Directory!, _stoppingToken))
        {
            if (code != 0)
            {
                Environment.Exit(code);
            }

            if (destination is null)
            {
                continue;
            }

            File.Copy(compiledUsh!.FullName, destination);
        }

        Environment.Exit(0);
    }
}
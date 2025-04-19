using Cocona;
using Microsoft.Extensions.Logging;
using Simpllist.Services;

namespace Simpllist.Commands;

/// <summary>
/// A CLI command to invoke the SIMPL+ compiler and build the .ush file from the generated usp.
/// </summary>
public sealed class CompileCommand
{
    private readonly ILogger<CompileCommand> _logger;
    private readonly Func<string, SimplPlusCompiler> _compilerFactory;

    /// <summary>
    /// Creates the new compile command
    /// </summary>
    /// <param name="logger">A logger for console output</param>
    /// <param name="compilerFactory">Provides a factory accepting a .usp file path.</param>
    public CompileCommand(ILogger<CompileCommand> logger, Func<string, SimplPlusCompiler> compilerFactory)
    {
        _logger = logger;
        _compilerFactory = compilerFactory;
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

        if (!path.EndsWith(".usp"))
        {
            _logger.LogCritical("Invalid SIMPL USP File {path}", path);
            Environment.Exit(ExitCodes.InvalidFileExtension);
        }

        _logger.LogInformation("Compiling SIMPL Plus USP {path}", path);

        if (!File.Exists(path))
        {
            _logger.LogCritical("File not found {path}", path);
            Environment.Exit(ExitCodes.FileNotFound);
        }

        using var compiler = _compilerFactory(path);

        var exitCode = await compiler.CompileSimplPlusModule();

        if (destination is null)
        {
            Environment.Exit(exitCode);
            return;
        }

        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        File.Copy(path.Replace(".usp", ".ush"), destination);
        Environment.Exit(exitCode);
    }
}
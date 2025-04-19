using Cocona;
using Microsoft.Extensions.Logging;
using Simpllist.Services;

namespace Simpllist.Commands;

public sealed class CompileCommand
{
    private readonly ILogger<CompileCommand> _logger;
    private readonly Func<string, SimplPlusCompiler> _compilerFactory;

    public CompileCommand(ILogger<CompileCommand> logger, Func<string, SimplPlusCompiler> compilerFactory)
    {
        _logger = logger;
        _compilerFactory = compilerFactory;
    }
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

        Environment.Exit(exitCode);
    }
}
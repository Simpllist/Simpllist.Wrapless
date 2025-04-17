using Cocona;
using Serilog;

namespace Simpllist.Commands;

public sealed class CompileCommand(ILogger logger)
{
    [Command("build", Aliases = new string[] { "b" }, Description = "Complies and creates the SIMPL+ wrapper from your C# source code")]
    public void CreateDriver(
        [Option(
            shortName: 'p',
            Description = "the assembly path",
            ValueName = "path")] string path,
        [Option(
            shortName: 'o',
            Description = "the output path for the compiled SIMPL+ wrapper",
            ValueName = "output")] string destination)
    {

       logger.Information("Executed Command");
    }
}
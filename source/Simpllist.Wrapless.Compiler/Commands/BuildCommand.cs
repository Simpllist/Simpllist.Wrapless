using System.Reflection;
using System.Text;
using Cocona;
using Microsoft.Extensions.Logging;
using Simpllist.Builder;

namespace Simpllist.Commands;

public sealed class BuildCommand
{

    [Command("build", Aliases = new string[] { "b" }, Description = "Complies and creates the SIMPL+ wrapper from your C# source code")]
    public async Task CreateDriver(
        [FromService] ILogger<CompileCommand> logger,
        [Option(
            shortName: 'p',
            Description = "the assembly path",
            ValueName = "path")] string path,
        [Option(
            shortName: 'o',
            Description = "the output path for the compiled SIMPL+ wrapper",
            ValueName = "output")] string? destination)
    {

        logger.LogInformation("Executed Command");
        var builder = new StringBuilder();

        var directory = new FileInfo(path).Directory; 
        var assembly = Assembly.LoadFrom(path);

        builder.AppendInformationBuilder(assembly);

        await File.WriteAllTextAsync(Path.Combine(directory!.FullName, "blah.usp"), builder!.ToString());
        
    }
}
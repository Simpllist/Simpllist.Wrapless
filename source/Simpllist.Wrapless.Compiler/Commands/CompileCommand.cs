using System.Reflection;
using Cocona;
using Serilog;
using Simpllist.Wrapless;

namespace Simpllist.Commands;

public sealed class CompileCommand
{
    [Command("build", Aliases = new string[] { "b" }, Description = "Complies and creates the SIMPL+ wrapper from your C# source code")]
    public async Task CreateDriver(
        [Option(
            shortName: 'p',
            Description = "the assembly path",
            ValueName = "path")] string path,
        [Option(
            shortName: 'o',
            Description = "the output path for the compiled SIMPL+ wrapper",
            ValueName = "output")] string? destination)
    {
        
        //logger.Information("Executed Command");
        
        var directory = new FileInfo(path).Directory; 
        var assembly = Assembly.LoadFrom(path);

        var moduleInformationType = assembly
            .GetTypes()
            .FirstOrDefault(t => t.IsClass && t.GetInterface("ISimplPlusModuleInformation") != null);

        if (moduleInformationType is null)
        {
            return;
        }

        var info = Activator.CreateInstance(moduleInformationType);

        if (info is ISimplPlusModuleInformation builder)
        {
            await File.WriteAllTextAsync(Path.Combine(directory!.FullName, "blah.usp"), builder.Builder.ToString());
        }
    }
}
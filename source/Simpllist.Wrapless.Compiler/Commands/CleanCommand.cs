using System.Diagnostics.CodeAnalysis;
using Cocona;
using Microsoft.Extensions.Logging;
using Simpllist.Services;
using System.IO;
using Simpllist.Manifest;

namespace Simpllist.Commands;

/// <summary>
/// The clean command should be used with extreme care as it will DELETE APP .ush and .usp and SPLs work in the provided directory.
/// </summary>
public sealed class CleanCommand
{
    private readonly ILogger<CleanCommand> _logger;

    /// <summary>
    /// Creates the command instance
    /// </summary>
    /// <param name="logger">a logger to log...</param>
    public CleanCommand(ILogger<CleanCommand> logger)
    {
        _logger = logger;
    }

    
    [Command("clean", Aliases = new string[] { "d" }, Description = "Deletes all the build artifacts from a previous run.")]
    public async Task ClearArtifacts(
        [Option(
            shortName: 'd',
            Description = "The target directory to search for build artifacts",
            ValueName = "directory")] string directory)
    {
        var artifacts = await Artifacts.LoadArtifactsFile(directory);

        artifacts?.CleanArtifacts();
    }
}
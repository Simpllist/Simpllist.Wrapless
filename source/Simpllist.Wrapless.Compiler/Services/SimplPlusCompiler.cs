using Dumpify;
using System.Diagnostics;
using Simpllist.Commands;

namespace Simpllist.Services;
public class SimplPlusCompiler : IDisposable
{
    private readonly string _userPlusModulePath;
    private readonly Process _process;

    private readonly string _fullExecutablePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Crestron\Simpl\SPlusCC.exe");
    
    public SimplPlusCompiler(string userPlusModulePath)
    {
        _userPlusModulePath = userPlusModulePath;

        if (!File.Exists(_fullExecutablePath))
        {
            Environment.Exit(ExitCodes.SimplPlusNotInstalled);
            return;
        }

        if (!_userPlusModulePath.EndsWith(".usp"))
        {
            Environment.Exit(ExitCodes.InvalidFileExtension);
            return;
        }

        _process = new Process();
        _process.ErrorDataReceived += Process_ErrorDataReceived;
        _process.OutputDataReceived += Process_OutputDataReceived;
        _process.Exited += Process_Exited;
    }

    

    public async Task<int> CompileSimplPlusModule()
    {
        var startInfo = new ProcessStartInfo(_fullExecutablePath,
        [
            "\\rebuild",
            _userPlusModulePath,
            "\\target",
            "series4"
        ]);

        _process.StartInfo = startInfo;

        _process.Start();
        await _process.WaitForExitAsync();

        return _process.ExitCode;
    }

    private static void Process_Exited(object? sender, EventArgs e)
    {
        e.DumpConsole();
    }

    private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        e.DumpConsole();
    }

    private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        e.DumpConsole();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _process.OutputDataReceived -= Process_OutputDataReceived;
        _process.Exited -= Process_Exited;
        _process.Dispose();
    }
}

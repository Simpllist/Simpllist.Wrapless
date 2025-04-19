using Dumpify;
using System.Diagnostics;

namespace Simpllist.Services;
internal class SPlusCompiler : IDisposable
{
    private readonly Process _process;

    private readonly string _fullExecutablePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Crestron\Simpl\SPlusCC.exe");
    
    public SPlusCompiler()
    {
        _process = new Process();
        _process.OutputDataReceived += Process_OutputDataReceived;
        _process.Exited += Process_Exited;
    }

    public int CompileSimplPlusModule(string filePath)
    {
        var startInfo = new ProcessStartInfo(_fullExecutablePath,
        [
            "\\rebuild",
            filePath,
            "\\target",
            "series4"
        ]);

        _process.StartInfo = startInfo;

        _process.Start();
        _process.WaitForExit();

        return _process.ExitCode;
    }

    private void Process_Exited(object? sender, EventArgs e)
    {
        e.DumpConsole();
    }

    private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
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

using Cocona;
using Dumpify;
using Microsoft.Extensions.DependencyInjection;
using Simpllist;
using Simpllist.Commands;
using Simpllist.Services;

try
{
    var builder = CoconaApp.CreateBuilder();

    builder.Services.AddLogging();

    // TODO: cancel token when terminal exist so we can stop compilations on large directories with several .usp files
    builder.Services.AddSingleton<CancellationTokenSource>(_ => new CancellationTokenSource(TimeSpan.FromMinutes(2)));

    builder.Services.AddTransient<Func<string, SimplPlusCompiler>>(_ =>
    {
        return Factory;

        SimplPlusCompiler Factory(string path) => new (path);
    });

    builder.Services.AddTransient<SimplPlusFileCompiler>();
    builder.Services.AddTransient<SimplPlusDirectoryCompiler>();

    var app = builder.Build();

    app.AddCommands<BuildCommand>();
    app.AddCommands<CleanCommand>();
    app.AddCommands<CompileCommand>();

    app.Run();

    return 0;
}
catch(Exception ex)
{
    ex.DumpConsole();
    return ExitCodes.Exception;
}

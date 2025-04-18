using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Simpllist.Commands;

try
{
    var builder = CoconaApp.CreateBuilder();

    builder.Services.AddSingleton((c) =>
        new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger());

    var app = CoconaApp.CreateBuilder().Build();

    app.AddCommands<CompileCommand>();

    app.Run();

    return 0;
}
catch(Exception ex)
{
    Console.WriteLine(ex);
    return -1;
}

using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Simpllist.Commands;

try
{
    var builder = CoconaApp.CreateBuilder();

    builder.Services.AddLogging();

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

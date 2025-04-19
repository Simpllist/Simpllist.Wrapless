using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Simpllist.Commands;
using Simpllist.Services;

try
{
    var builder = CoconaApp.CreateBuilder();

    builder.Services.AddLogging();

    builder.Services.AddTransient<Func<string, SimplPlusCompiler>>(_ =>
    {
        return Factory;

        SimplPlusCompiler Factory(string path) => new SimplPlusCompiler(path);
    });

    var app = builder.Build();

    app.AddCommands<BuildCommand>();
    app.AddCommands<CompileCommand>();

    app.Run();

    return 0;
}
catch(Exception ex)
{
    Console.WriteLine(ex);
    return -1;
}

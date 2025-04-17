using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Simpllist.Commands;


var builder = CoconaApp.CreateBuilder();

builder.Services.AddSingleton((c) => 
    new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger());

var app = CoconaApp.CreateBuilder().Build();

app.AddCommands<CompileCommand>();

app.Run();
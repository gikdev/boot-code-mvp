using Backend.Api;
using Backend.App;
using Backend.Infra;
using DotNetEnv;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);
var config  = builder.Configuration;

builder.Host
    .ConfigLoggingStuff(config);

builder.Services
    .AddApiStuff()
    .AddAppStuff()
    .AddInfraStuff();

builder
    .Build()
    .UseApiStuff()
    .Run();

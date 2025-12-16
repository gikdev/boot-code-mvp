using Backend.Api;
using Backend.App;
using Backend.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiStuff()
    .AddAppStuff()
    .AddInfraStuff();

builder
    .Build()
    .UseApiStuff()
    .Run();

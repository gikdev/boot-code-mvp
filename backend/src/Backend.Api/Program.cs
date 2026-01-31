using Backend.Api;
using Backend.App;
using Backend.Infra;
using DotNetEnv;
using Npgsql;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);
var config  = builder.Configuration;
var connStr = new NpgsqlConnectionStringBuilder {
    Host     = config.GetValue<string?>("DB_HOST")     ?? throw new Exception("DB_HOST not set"),
    Port     = config.GetValue<int?>("DB_PORT")        ?? throw new Exception("DB_PORT not set"),
    Database = config.GetValue<string?>("DB_NAME")     ?? throw new Exception("DB_NAME not set"),
    Username = config.GetValue<string?>("DB_USER")     ?? throw new Exception("DB_USER not set"),
    Password = config.GetValue<string?>("DB_PASSWORD") ?? throw new Exception("DB_PASSWORD not set")
}.ConnectionString;

builder.Host
    .ConfigLoggingStuff(config);

builder.Services
    .ConfigStuff()
    .AddApiStuff()
    .AddAppStuff()
    .AddInfraStuff(connStr);

builder
    .Build()
    .UseApiStuff()
    .Run();

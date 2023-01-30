using Tango.Api.Endpoints;
using Tango.Api.Repository.Sqlite;
using Tango.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite();
builder.Services.AddServices();
builder.Services.AddEndpoints();

var app = builder.Build();

app.AddEndpoints();

await InitializeDatabase.ExecuteAsync();

app.Run();

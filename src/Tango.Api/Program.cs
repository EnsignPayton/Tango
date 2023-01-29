using Tango.Api;
using Tango.Api.Common;
using Tango.Api.Repository.Memory;
using Tango.Api.Repository.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("WaniKani");

// builder.Services.AddInMemoryRepositories();
builder.Services.AddSqlite();
builder.Services.AddEndpoints();

builder.Services.AddSingleton<TempWaniKaniQuery>();

var app = builder.Build();

app.AddEndpoints();

await InitializeDatabase.ExecuteAsync();
// await app.Services.GetRequiredService<TempWaniKaniQuery>().ExecuteAsync();

app.Run();

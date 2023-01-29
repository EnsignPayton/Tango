using Tango.Api;
using Tango.Api.Common;
using Tango.Api.Repository.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("WaniKani");

builder.Services.AddInMemoryRepositories();
builder.Services.AddEndpoints();

builder.Services.AddSingleton<DummyDataProvider>();
builder.Services.AddSingleton<TempWaniKaniQuery>();

var app = builder.Build();

await app.Services.GetRequiredService<DummyDataProvider>().ExecuteAsync();
// await app.Services.GetRequiredService<TempWaniKaniQuery>().ExecuteAsync();

app.AddEndpoints();

app.Run();

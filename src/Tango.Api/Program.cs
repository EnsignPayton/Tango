using Tango.Api;
using Tango.Api.Common;
using Tango.Api.Custom;
using Tango.Api.WaniKani;
using Tango.Api.Words;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustom();
builder.Services.AddWaniKani();
builder.Services.AddWords();
builder.Services.AddEndpoints();
builder.Services.AddSingleton<DummyDataProvider>();

var app = builder.Build();

await app.Services.GetRequiredService<DummyDataProvider>().ExecuteAsync();

app.AddEndpoints();

app.Run();

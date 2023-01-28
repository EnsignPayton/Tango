using Tango.Api.Common;
using Tango.Api.Words;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWords();
builder.Services.AddEndpoints();

var app = builder.Build();

app.AddEndpoints();

app.Run();

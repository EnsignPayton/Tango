using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Tango.Anki;
using Tango.Core;
using Tango.FrontendExample;
using Tango.FrontendExample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<VocabManager>();

var app = builder.Build();

var vm = app.Services.GetRequiredService<VocabManager>();

try
{
    var ankiLoader = new AnkiWordLoader();
    var ankiWords = await ankiLoader.LoadWordsAsync();
    vm.Words = ankiWords.ToList();
}
catch (Exception ex)
{
    vm.Words = new List<Word>();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
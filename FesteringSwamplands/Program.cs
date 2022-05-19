using Codex;
using Codex.NameModification;
using Codex.WordRetrieval;
using FesteringSwamplands;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Spotify;
using Synger.Github;
using System.Net;
using Terra;
using Terra.Agolora;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IWorldReporter, WorldReporter>();
builder.Services.AddTransient<IWorldAlterer, WorldAlterer>();
builder.Services.AddTransient<IPopulationReporter, PopulationReporter>();
builder.Services.AddTransient<IPartitionAlterer, PopulationFuzzer>();
builder.Services.AddTransient<IWordRetriever, WordRetriever>();
builder.Services.AddTransient<SeededMarkovNamer>();
builder.Services.AddTransient<LanguageGenerator>();
builder.Services.AddTransient<ModificationGenerator>();
builder.Services.AddSingleton<HttpListener>();
builder.Services.AddSingleton<GitHubHelper>();
builder.Services.AddSingleton<SpotifyHelper>();
builder.Services.AddSingleton<Namer>();

await builder.Build().RunAsync();
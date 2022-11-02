using Codex;
using Codex.NameModification;
using Codex.WordRetrieval;
using FesteringSwamplands;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Spotify;
using Synger.Github;
using Synger.Spotify;
using System.Net;
using Terra.V1.Agolora;
using Terra.V1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient();

//builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

builder.Services.AddTransient<ISpotifySongResolver, SpotifySongResolver>();

builder.Services.AddTransient<IWorldReporter, WorldReporter>();
builder.Services.AddTransient<IWorldAlterer, WorldAlterer>();
builder.Services.AddTransient<IPopulationReporter, PopulationReporter>();
builder.Services.AddTransient<IPartitionAlterer, PopulationFuzzer>();
builder.Services.AddTransient<IWordRetriever, WordRetriever>();
builder.Services.AddTransient<SeededMarkovNamer>();
builder.Services.AddTransient<LanguageGenerator>();
builder.Services.AddTransient<ModificationGenerator>();
builder.Services.AddSingleton<Namer>();

await builder.Build().RunAsync();
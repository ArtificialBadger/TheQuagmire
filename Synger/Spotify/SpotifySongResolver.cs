using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Synger.Spotify
{
    public class SpotifySongResolver : ISpotifySongResolver
    {
        private readonly IConfiguration configuration;
        //private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;

        public SpotifySongResolver(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
        }

        public void GetSong()
        {
            var clientId = this.configuration["SpotifyClientId"];
            var redirectUri = this.configuration["SpotifyRedirectUri"];

            //var client = this.httpClientFactory.CreateClient();
            //client.red
        }

        public string GetClientId()
        {
            return this.configuration["SpotifyClientId"];
        }

        public async Task Authorize()
        {
            var clientId = this.configuration["SpotifyClientId"];
            var scope = "user-read-private";
            var redirectUri = "localhost:44321";
            var state = Guid.NewGuid().ToString();

            await httpClient.GetAsync($"https://accounts.spotify.com/authorize?response_type=code&client_id={clientId}&scope={scope}&redirect_uri={redirectUri}&state={state}");
        }
    }
}

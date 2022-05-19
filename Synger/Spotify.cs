using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Synger.Github;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Spotify
{
    public class SpotifyHelper
    {
        private IConfiguration configuration;

        private HttpClient client;
        private HttpListener listener;

        private GitHubHelper github;

        public SpotifyHelper(HttpListener listener, IConfiguration configuration, HttpClient client, GitHubHelper github)
        {
            this.listener = listener;
            this.configuration = configuration;
            this.client = client;
            this.github = github;
        }

        private string Token;

        public async Task SetGithubStatus()
        {
            if (this.Token != null)
            {
                await this.NowToken(this.Token);
                return;
            }

            var url = $"https://accounts.spotify.com/authorize?response_type=code&client_id={clientId}&redirect_uri={HttpUtility.UrlEncode(redirectUri)}&scope={HttpUtility.UrlEncode(String.Join(',', scopes))}&code_challenge={hash}&code_challenge_method=S256";

            listener.Prefixes.Add("http://localhost:8081/");
            listener.Start(); // start server (Run application as Administrator!)
            
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); // Works on windows
            
            await Task.Run(async () => await ResponseThread());
        }

        public static List<string> scopes = new List<string>() { "user-read-currently-playing", "user-read-playback-state" };

        public string clientId => configuration["SpotifyClientId"];
        public static string redirectUri = "http://localhost:8081";

        public static string secret = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        public static string hash = GetEncodedHash(secret);

        

        public async Task ResponseThread()
        {
            while (listener.IsListening)
            {
                HttpListenerContext context = listener.GetContext(); // get a context
                                                                     // Now, you'll find the request URL in context.Request.Url
                byte[] _responseArray = Encoding.UTF8.GetBytes("Auth Successful! Feel free to close this tab."); // get the bytes to response
                context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
                context.Response.KeepAlive = false; // set the KeepAlive bool to false
                context.Response.Close(); // close the connection
                var url = context.Request.Url.Query;
                if (url.Contains("?code="))
                {
                    var code = url.Substring(6);
                    await NowCode(code);

                    //context.Response.StatusCode(@"https://www.google.com");
                    ////context.Response.Redirect(@"javascript:alert(""hi"");");
                    //context.Response.KeepAlive = false; // set the KeepAlive bool to false
                    //context.Response.Close();

                    listener.Stop();
                    listener.Close();
                    break;
                }
            }
        }

        public async Task NowCode(string code)
        {            
            var url = $"https://accounts.spotify.com/api/token";
            var parameters = new Dictionary<string, string>()
                {
                    {"client_id", clientId},
                    {"grant_type", "authorization_code"},
                    {"code", code},
                    {"code_verifier", secret},
                    {"redirect_uri", redirectUri}
                };

            using var content = new FormUrlEncodedContent(parameters);

            var response = await client.PostAsync(url, content);

            var token = await JsonSerializer.DeserializeAsync<AuthThing>(await response.Content.ReadAsStreamAsync());

            this.Token = token.access_token;

            await NowToken(token.access_token);
        }

        public async Task NowToken(string token)
        {
            using var message = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/player/currently-playing");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(message);

            var song = await JsonSerializer.DeserializeAsync<Song>(await response.Content.ReadAsStreamAsync());

            await github.UpdateStatus($"{song.Item.Name} - {String.Join(", ", song.Item.Artists.Select(x => x.Name))}");

        }

        public async Task<Song> GetSong()
        {
            using var message = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/player/currently-playing");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.Token);

            var response = await client.SendAsync(message);

            var song = await JsonSerializer.DeserializeAsync<Song>(await response.Content.ReadAsStreamAsync());

            return song;
        }

        private class AuthThing
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            //public string expires_in {get; set;}

            public string refresh_token { get; set; }

            public string scope { get; set; }
        }

        public static string GetEncodedHash(string secret)
        {
            using var hasher = SHA256.Create();
            hasher.ComputeHash(Encoding.UTF8.GetBytes(secret));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hasher.Hash.Length; i++)
            {
                builder.Append($"{hasher.Hash[i]:X2}");
                if ((i % 4) == 3) builder.Append(" ");
            }

            var encoded = Base64UrlEncoder.Encode(hasher.Hash);

            return encoded;
        }

        public partial class Song
        {
            [JsonPropertyName("timestamp")]
            public long Timestamp { get; set; }

            [JsonPropertyName("context")]
            public Context Context { get; set; }

            [JsonPropertyName("progress_ms")]
            public long ProgressMs { get; set; }

            [JsonPropertyName("item")]
            public Item Item { get; set; }

            [JsonPropertyName("currently_playing_type")]
            public string CurrentlyPlayingType { get; set; }

            [JsonPropertyName("actions")]
            public Actions Actions { get; set; }

            [JsonPropertyName("is_playing")]
            public bool IsPlaying { get; set; }
        }

        public partial class Actions
        {
            [JsonPropertyName("disallows")]
            public Disallows Disallows { get; set; }
        }

        public partial class Disallows
        {
            [JsonPropertyName("resuming")]
            public bool Resuming { get; set; }
        }

        public partial class Context
        {
            [JsonPropertyName("external_urls")]
            public ExternalUrls ExternalUrls { get; set; }

            [JsonPropertyName("href")]
            public Uri Href { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("uri")]
            public string Uri { get; set; }
        }

        public partial class ExternalUrls
        {
            [JsonPropertyName("spotify")]
            public Uri Spotify { get; set; }
        }

        public partial class Item
        {
            [JsonPropertyName("album")]
            public Album Album { get; set; }

            [JsonPropertyName("artists")]
            public Artist[] Artists { get; set; }

            [JsonPropertyName("available_markets")]
            public string[] AvailableMarkets { get; set; }

            [JsonPropertyName("disc_number")]
            public long DiscNumber { get; set; }

            [JsonPropertyName("duration_ms")]
            public long DurationMs { get; set; }

            [JsonPropertyName("explicit")]
            public bool Explicit { get; set; }

            [JsonPropertyName("external_ids")]
            public ExternalIds ExternalIds { get; set; }

            [JsonPropertyName("external_urls")]
            public ExternalUrls ExternalUrls { get; set; }

            [JsonPropertyName("href")]
            public Uri Href { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("is_local")]
            public bool IsLocal { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("popularity")]
            public long Popularity { get; set; }

            [JsonPropertyName("preview_url")]
            public Uri PreviewUrl { get; set; }

            [JsonPropertyName("track_number")]
            public long TrackNumber { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("uri")]
            public string Uri { get; set; }
        }

        public partial class Album
        {
            [JsonPropertyName("album_type")]
            public string AlbumType { get; set; }

            [JsonPropertyName("artists")]
            public Artist[] Artists { get; set; }

            [JsonPropertyName("available_markets")]
            public string[] AvailableMarkets { get; set; }

            [JsonPropertyName("external_urls")]
            public ExternalUrls ExternalUrls { get; set; }

            [JsonPropertyName("href")]
            public Uri Href { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("images")]
            public Image[] Images { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("release_date")]
            public DateTimeOffset ReleaseDate { get; set; }

            [JsonPropertyName("release_date_precision")]
            public string ReleaseDatePrecision { get; set; }

            [JsonPropertyName("total_tracks")]
            public long TotalTracks { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("uri")]
            public string Uri { get; set; }
        }

        public partial class Artist
        {
            [JsonPropertyName("external_urls")]
            public ExternalUrls ExternalUrls { get; set; }

            [JsonPropertyName("href")]
            public Uri Href { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("uri")]
            public string Uri { get; set; }
        }

        public partial class Image
        {
            [JsonPropertyName("height")]
            public long Height { get; set; }

            [JsonPropertyName("url")]
            public Uri Url { get; set; }

            [JsonPropertyName("width")]
            public long Width { get; set; }
        }

        public partial class ExternalIds
        {
            [JsonPropertyName("isrc")]
            public string Isrc { get; set; }
        }
    }
}

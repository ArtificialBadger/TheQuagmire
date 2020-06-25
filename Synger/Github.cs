using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Synger
{
	namespace Github
	{
		public static class Constants
		{
			internal static string innerQuery = @"{
			  viewer {
			    status {
			      createdAt
			      message
			      indicatesLimitedAvailability
			      id
			      expiresAt
			      updatedAt
			      user {
			        id
			        name
			        login
			      }
			    }
			  }
			}";


			internal static string Mutation = @"
			mutation Status {
				__typename
				changeUserStatus(input: {message: ""##newStatus""}) { clientMutationId}
			}
			";
		}

		public class GitHubHelper
		{
			private HttpClient client;
			private IConfiguration config;

			public partial class DataClass
			{
				public Data Data { get; set; }
			}

			public partial class Data
			{
				public Viewer Viewer { get; set; }
			}

			public partial class Viewer
			{
				public Status Status { get; set; }
			}

			public class Status
			{
				public DateTime? CreatedAt { get; set; }

				public string Message { get; set; }

				public bool IndicatesLimitedAvailability { get; set; }

				public DateTime? ExpiresAt { get; set; }

				public User User { get; set; }

				public string Id { get; set; }
			}

			public async Task UpdateStatus(string newStatus)
			{
				using var message = new HttpRequestMessage(HttpMethod.Post, gitHubApiBaseUrl);

				message.Headers.Authorization = new AuthenticationHeaderValue("token", gitHubToken);
				message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
				message.Headers.UserAgent.Add(new ProductInfoHeaderValue("Awesome-Octocat-App", "1"));

				var query = new Query() { query = Constants.Mutation.Replace("##newStatus", newStatus) };
				message.Content = new StringContent(JsonSerializer.Serialize(query));

				using var response = await client.SendAsync(message);

				var json = await response.Content.ReadAsStringAsync();

				Console.WriteLine($"Set Github Status to {newStatus}");
			}

			private class Query
			{
				public string query { get; set; }
			}

			private static JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};

			public class User
			{
				public string Id { get; set; }

				public string Login { get; set; }

				public string Bio { get; set; }

				public string Company { get; set; }
			}

			private string gitHubToken => config["GitHubToken"]; 
			
			private const string gitHubApiBaseUrl = "https://api.github.com/graphql";

			public GitHubHelper(HttpClient client, IConfiguration config)
			{
				this.client = client;
				this.config = config;
			}
		}
	}
}

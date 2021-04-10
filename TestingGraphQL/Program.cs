using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using GraphQL.Client;

namespace TestingGraphQL
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("http://echo.somee.com/netflixgraph/graphql"),
            };

            var client = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());

            var query = "{ actionShows { casts } }";

            var request = new GraphQLHttpRequest
            { 
                Query = query
            };

            var graphQLResponse = await client.SendQueryAsync<object>(request);


            var stringResult = graphQLResponse.Data.ToString();
            //stringResult = stringResult.Replace($"\"actionShows\":", string.Empty);
            //stringResult = stringResult.Remove(0, 1);
            //stringResult = stringResult.Remove(stringResult.Length - 1, 1);

            var result = JsonConvert.DeserializeObject<Data>(stringResult.ToString());

            for (int i = 0; i < result.ActionShows.Length; i++)
            {
                Console.WriteLine(result.ActionShows[i].Casts);
            }
            Console.Read();

        }
    }

    public class GithubUser
    {
        public string Name { get; }

        public string Company { get; }

        public DateTimeOffset CreatedAt { get; }

        public long FollowersCount { get; }

    }

    public partial class Welcome
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("actionShows")]
        public ActionShow[] ActionShows { get; set; }
    }

    public partial class ActionShow
    {
        [JsonProperty("casts")]
        public string Casts { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("genre")]
        public string[] Genre { get; set; }

        [JsonProperty("isFeatured")]
        public bool IsFeatured { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("isPopular")]
        public bool IsPopular { get; set; }

        [JsonProperty("isAction")]
        public bool IsAction { get; set; }

        [JsonProperty("isComedy")]
        public bool IsComedy { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("infoThumbnail")]
        public Uri InfoThumbnail { get; set; }

        [JsonProperty("isComingSoon")]
        public bool IsComingSoon { get; set; }
    }
}

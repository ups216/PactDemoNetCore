using System;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Consumer
{
    public class Something
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
    }
    public class SomethingApiClient
    {
        private readonly HttpClient _client;

        public SomethingApiClient(string baseUri = null)
        {
            _client = new HttpClient {BaseAddress = new Uri(baseUri ?? "http://my-api")};
        }

        public async Task<Something> GetSomething(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/somethings/" + id);
            request.Headers.Add("Accept", "applciation/json");

            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var status = response.StatusCode;

            var reasonPhrase = response.ReasonPhrase;

            request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK)
            {
                return !string.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<Something>(content) : null;
            }

            throw new Exception(reasonPhrase);
        }
        
    }
}
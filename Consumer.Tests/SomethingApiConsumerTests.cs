using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;
using Consumer;
using Newtonsoft.Json;

namespace Consumer.Tests
{
    public class SomethingApiConsumerTests: IClassFixture<ConsumerApiMock>
    {
        private readonly IMockProviderService _mockProviderService;
        private string _serviceUri;

        public SomethingApiConsumerTests(ConsumerApiMock data)
        {
            _mockProviderService = data.MockProviderService;
            //_mockProviderService.ClearInteractions();
            _serviceUri = data.MockProviderServiceBaseUri;
        }
        
        [Fact]
        public async Task GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            string expectedId = "tester";
            // Arrange
            _mockProviderService
                .Given("There is a something with id 'tester'")
                .UponReceiving("A Get request to retrieve the Id of something")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/somethings/{expectedId}",
                    Headers = new Dictionary<string, object>
                    {
                        {"Accept", "applciation/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        Id = "tester",
                        FirstName = "Totally",
                        LastName = "Awesome"
                    }
                });
            var consumer = new SomethingApiClient(_serviceUri);
            // var httpClient = new HttpClient();
            // var response = await httpClient.GetAsync($"{_serviceUri}/somethings/tester");
            // var json = await response.Content.ReadAsStringAsync();
            // var something = JsonConvert.DeserializeObject<Something>(json);

            // Act
            var result = await consumer.GetSomething("tester");
            
            // Assert
            Assert.Equal(expectedId, result.Id);
            Assert.Equal("Totally", result.FirstName);
            Assert.Equal("Awesome", result.LastName);
            
            _mockProviderService.VerifyInteractions();
        }
    }
}
using System;
using System.IO;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;

namespace Consumer.Tests
{
    public class ConsumerApiMock : IDisposable
    {

        public readonly IPactBuilder _pactBuilder;
        public IMockProviderService MockProviderService { get; }
        public int MockServerPort
        {
            get { return 9222; }
        }
        public string MockProviderServiceBaseUri { get {
            return String.Format("http://localhost:{0}", MockServerPort);
        }}

        public ConsumerApiMock()
        {
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                LogDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}",
                PactDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}"
            };

            _pactBuilder = new PactBuilder(pactConfig)
                .ServiceConsumer("SomethingApiClient")
                .HasPactWith("GetSomething");

            MockProviderService = _pactBuilder.MockService(MockServerPort, false, IPAddress.Any);
        }

        private bool _disposed = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _pactBuilder.Build();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
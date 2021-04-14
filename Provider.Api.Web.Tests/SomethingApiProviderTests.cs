using System;
using System.Collections.Generic;
using System.IO;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Owin.Hosting;

namespace Provider.Api.Web.Tests
{
    public class SomethingApiProviderTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        
        public SomethingApiProviderTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void EnsureApiHonoursPactWithConsumer()
        {
            const string serviceUri = "http://localhost:5001";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                }
            };
            new PactVerifier(config)
                .ServiceProvider("Something", serviceUri)
                .HonoursPactWith("GetSomething")
                .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}somethingapiclient-getsomething.json")
                .Verify();
        }

        public virtual void Dispose()
        {
        }
    }
}
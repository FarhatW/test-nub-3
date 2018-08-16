using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TestJCE.IntegrationTests.Tests
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer _testServer;
        public HttpClient HttpClient { get; }

        public TestFixture()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<TStartup>();
            _testServer = new TestServer(webHostBuilder);

            HttpClient = _testServer.CreateClient();
            HttpClient.BaseAddress = new Uri("http://localhost");
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _testServer.Dispose();
        }
    }
}

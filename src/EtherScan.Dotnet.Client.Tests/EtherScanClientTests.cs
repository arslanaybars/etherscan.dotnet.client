using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;

namespace EtherScan.Dotnet.Client.Tests
{
    public class EtherScanClientTests
    {
        protected HttpClient CreateMockHttpClient<T>(T expectedResponse)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = expectedResponse == null
                        ? new StringContent(string.Empty)
                        : JsonContent.Create(expectedResponse)
                });

            return new HttpClient(mockHttpMessageHandler.Object);
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly string _fakeResponse;

    public FakeHttpMessageHandler(string fakeResponse)
    {
        _fakeResponse = fakeResponse;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(_fakeResponse)
        };
        return Task.FromResult(response);
    }
}

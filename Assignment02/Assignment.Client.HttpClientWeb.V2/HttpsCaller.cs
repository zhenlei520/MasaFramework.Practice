namespace Assignment.Client.HttpClientWeb.V2;

public class HttpsCaller : HttpClientCallerBase
{
    public HttpsCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override string BaseAddress { get; set; } = "https://localhost:5001";

    protected override IHttpClientBuilder UseHttpClient()
        => base.UseHttpClient().AddHttpMessageHandler<HttpClientDelegatingHandler>();

    public Task<string?> GetHello2Async() 
        => CallerProvider.PostAsync<object, string>("Hello2", new { });
}

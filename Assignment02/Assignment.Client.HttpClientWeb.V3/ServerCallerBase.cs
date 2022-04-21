namespace Assignment.Client.HttpClientWeb.V3;

public abstract class ServerCallerBase: HttpClientCallerBase
{
    protected ServerCallerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override string BaseAddress { get; set; } = "https://localhost:5001";

    protected override IHttpClientBuilder UseHttpClient()
        => base.UseHttpClient().AddHttpMessageHandler<HttpClientDelegatingHandler>();
}

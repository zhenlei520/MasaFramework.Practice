namespace Assignment.Client.HttpClientWeb.V3;

public class HttpCaller : ServerCallerBase
{
    public HttpCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public Task<string> GetHello1Async() => CallerProvider.GetStringAsync("Hello1");
}

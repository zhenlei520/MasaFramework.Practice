namespace Assignment.Client.HttpClientWeb.V3;

public class HttpsCaller : ServerCallerBase
{
    public HttpsCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public Task<string?> GetHello2Async()
        => CallerProvider.PostAsync<object, string>("Hello2", new { });
}

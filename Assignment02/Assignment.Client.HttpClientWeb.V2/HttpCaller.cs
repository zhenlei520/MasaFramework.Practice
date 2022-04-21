namespace Assignment.Client.HttpClientWeb.V2;

public class HttpCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; } = "http://localhost:5000";
    
    public HttpCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public Task<string> GetHello1Async() => CallerProvider.GetStringAsync("Hello1");
}

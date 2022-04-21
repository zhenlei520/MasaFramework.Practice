using Masa.Utils.Caller.HttpClient;

namespace Assignment.Client.HttpClientWeb.V3;

public abstract class ServerCallerBase: HttpClientCallerBase
{
    protected override string BaseAddress { get; set; } = "http://localhost:5000";

    protected ServerCallerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 重写UserHttpClient，可以使用委托处理程序，也可以调整默认请求超时时间等配置，不需要可以删除
    /// </summary>
    /// <returns></returns>
    protected override IHttpClientBuilder UseHttpClient()
        => base.UseHttpClient().AddHttpMessageHandler<HttpClientDelegatingHandler>();
}

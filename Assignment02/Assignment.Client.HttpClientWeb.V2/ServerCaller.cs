using Masa.Utils.Caller.HttpClient;

namespace Assignment.Client.HttpClientWeb.V2;

public class ServerCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; } = "http://localhost:5000";

    public ServerCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 重写UserHttpClient，可以使用委托处理程序，也可以调整默认请求超时时间等配置，不需要可以删除
    /// </summary>
    /// <returns></returns>
    protected override IHttpClientBuilder UseHttpClient()
        => base.UseHttpClient().AddHttpMessageHandler<HttpClientDelegatingHandler>();

    /// <summary>
    /// 调用服务获取用户信息
    /// </summary>
    /// <param name="id">用户id</param>
    /// <returns></returns>
    public Task<UserDto?> GetUserAsync(int id)
        => CallerProvider.GetAsync<object, UserDto>("User", new { id = id });

    /// <summary>
    /// 调用服务添加用户
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public Task<string?> AddUserAsync(string userName)
        => CallerProvider.PostAsync<object, string>("User", new { Name = userName });
}

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

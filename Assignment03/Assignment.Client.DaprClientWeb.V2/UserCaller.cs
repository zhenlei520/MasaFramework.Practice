namespace Assignment.Client.DaprClientWeb.V2;

public class UserCaller : ServerCallerBase
{
    public UserCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

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

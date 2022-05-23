using Masa.Utils.Caller.DaprClient;

namespace Assignment.Client.DaprClientWeb.V2;

/// <summary>
/// 注意：ServerCallerBase是抽象类哟（抽象类不会被DI注册）, 与使用Caller.HttpClient相比，需要修改的是继承的基类改为DaprCallerBase
/// </summary>
public abstract class ServerCallerBase : DaprCallerBase
{
    protected override string AppId { get; set; } = "Assignment-Server";

    public ServerCallerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

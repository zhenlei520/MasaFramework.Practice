using Masa.Utils.Caller.DaprClient;

namespace Assignment.Client.DaprClientWeb.V2;

public abstract class ServerCallerBase : DaprCallerBase
{
    protected override string AppId { get; set; } = "Assignment-Server";

    public ServerCallerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

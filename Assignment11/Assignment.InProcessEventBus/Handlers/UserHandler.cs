using Assignment.InProcessEventBus.Events;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events.Enums;

namespace Assignment.InProcessEventBus.Handlers;

public class UserHandler
{
    private readonly ILogger<UserHandler>? _logger;

    public UserHandler(ILogger<UserHandler>? logger = null)
    {
        _logger = logger;
    }

    [EventHandler(1)]
    public void RegisterUser(RegisterUserEvent @event)
    {
        _logger?.LogDebug("-----------{Message}-----------", "检测用户是否存在并注册用户");
        //todo: 编写注册用户业务
    }

    [EventHandler(2)]
    public void SendAwardByRegister(RegisterUserEvent @event)
    {
        _logger?.LogDebug("-----------{Account} 注册成功 {Message}-----------", @event.Account, "发送邮件提示注册成功");
        //todo: 编写发送奖励等
    }

    [EventHandler(1, IsCancel = true)]
    public void CancelSendAwardByRegister(RegisterUserEvent @event)
    {
        _logger?.LogDebug("-----------{Account} 注册成功，发放奖励失败 {Message}-----------", @event.Account, "发放奖励补偿");
    }

    [EventHandler(3, FailureLevels = FailureLevels.Ignore)]
    public void SendNoticeByRegister(RegisterUserEvent @event)
    {
        _logger?.LogDebug("-----------{Account} 注册成功 {Message}-----------", @event.Account, "发送邮件提示注册成功");
        //todo: 编写发送注册通知等
        throw new Exception("取消");
    }
}

using Assignment.InProcessEventBus.Events;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events.Enums;

namespace Assignment.InProcessEventBus.Handlers;

// public class TransferHandler
// {
//     // [EventHandler]
//     // public Task TransferAsync(TransferEvent @event)
//     // {
//     //     //todo: 处理转账业务
//     //     return Task.CompletedTask;
//     // }
//
//     /// <summary>
//     /// 检查参数信息
//     /// </summary>
//     /// <param name="event"></param>
//     /// <returns></returns>
//     /// <exception cref="ArgumentException"></exception>
//     [EventHandler(0)]
//     public Task CheckTransferEventParameter(TransferEvent @event)
//     {
//         if (@event.Money <= 0)
//             throw new ArgumentException("转账金额必须大于0");
//         if (string.IsNullOrEmpty(@event.Account))
//             throw new ArgumentException("转账账户错误");
//         if (string.IsNullOrEmpty(@event.ReceiveAccount))
//             throw new ArgumentException("到账账户错误");
//
//         return Task.CompletedTask;
//     }
//
//     [EventHandler(1)]
//     public Task CheckAccountState(TransferEvent @event)
//     {
//         // todo: 第一步: 检查当前余额状态，账户余额
//         return Task.CompletedTask;
//     }
//
//     [EventHandler(2)]
//     public Task FreezeMoneyAsync(TransferEvent @event)
//     {
//         //todo: 第二步: 冻结余额
//         return Task.CompletedTask;
//     }
//
//     // [EventHandler(3)]
//     // public Task TransferAsync(TransferEvent @event)
//     // {
//     //     try
//     //     {
//     //         //todo: 发起转账请求
//     //     }
//     //     catch (Exception ex)
//     //     {
//     //         //todo: 发起转账请求异常则解冻余额
//     //     }
//     //     return Task.CompletedTask;
//     // }
//
//     [EventHandler(3, FailureLevels.ThrowAndCancel)]
//     public Task TransferAsync(TransferEvent @event)
//     {
//         //todo: 发起转账请求
//         return Task.CompletedTask;
//     }
//
//     /// <summary>
//     /// IsCancel为true，代表当前Handler为Cancel，默认IsCancel为false
//     /// </summary>
//     /// <param name="event"></param>
//     /// <returns></returns>
//     [EventHandler(3, IsCancel = true)]
//     public Task UnfreezeMoneyAsync(TransferEvent @event)
//     {
//         //todo: 发起转账请求异常, 解冻余额
//         return Task.CompletedTask;
//     }
// }

public class TransferHandler : IEventHandler<TransferEvent>
{
    public Task HandleAsync(TransferEvent @event)
    {
        //todo: 处理转账业务
        return Task.CompletedTask;
    }
}

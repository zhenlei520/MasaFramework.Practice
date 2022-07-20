using System.Globalization;
using Assignment.Client.HttpClientWeb.V2;
using Masa.Utils.Caller.Core;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCaller();

#region 非必须 (如果不需要[委托处理程序](https: //docs.microsoft.com/zh-cn/dotnet/api/system.net.http.delegatinghandler?view=net-6.0) 可以注释以下代码)

// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// builder.Services.AddTransient<HttpClientDelegatingHandler>();

#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello HttpClientWeb.V2!");

app.MapGet("/Test/User/Get", async ([FromServices] ServerCaller serverCaller) =>
{
    var id = new Random().Next(1, 10);//默认用户id
    var user = await serverCaller.GetUserAsync(id);
    return $"获取用户信息成功：用户名称为：{user!.Name}";
});

app.MapGet("/Test/User/Add", async ([FromServices] ServerCaller serverCaller) =>
{
    var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
    string timeSpan = dateTimeOffset.ToUnixTimeSeconds().ToString();
    var userName = "ss_" + timeSpan; //模拟一个用户名
    string? response= await serverCaller.AddUserAsync(userName);
    return $"创建用户成功了，用户名称为：{response}";
});

app.Run();

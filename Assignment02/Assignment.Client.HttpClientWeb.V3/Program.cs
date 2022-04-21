using System.Globalization;
using Assignment.Client.HttpClientWeb.V3;
using Masa.Utils.Caller.Core;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCaller();

#region 非必须 (如果不需要[委托处理程序](https: //docs.microsoft.com/zh-cn/dotnet/api/system.net.http.delegatinghandler?view=net-6.0) 可以注释以下代码)

// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// builder.Services.AddScoped<HttpClientDelegatingHandler>();

#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello HttpClientWeb.V2!");

app.MapGet("/Test/User/Get", async ([FromServices] UserCaller caller) =>
{
    var id = new Random().Next(1, 10);//默认用户id
    var user = await caller.GetUserAsync(id);
    return $"获取用户信息成功：用户名称为：{user!.Name}";
});

app.MapGet("/Test/User/Add", async ([FromServices] UserCaller caller) =>
{
    string timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds.ToString(CultureInfo.InvariantCulture);
    var userName = "ss_" + timeSpan; //模拟一个随机的用户名
    string? response= await caller.AddUserAsync(userName);
    return $"创建用户成功了，{response}";
});

app.Run();

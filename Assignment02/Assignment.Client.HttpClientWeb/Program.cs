using System.Globalization;
using Masa.Utils.Caller.Core;
using Masa.Utils.Caller.HttpClient;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

#region 非必须 (如果不需要[委托处理程序](https: //docs.microsoft.com/zh-cn/dotnet/api/system.net.http.delegatinghandler?view=net-6.0) 可以注释以下代码)

// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// builder.Services.AddScoped<HttpClientDelegatingHandler>();

#endregion

builder.Services.AddCaller(option =>
{

    #region 不使用委托处理程序

    option.UseHttpClient(opt =>
    {
        opt.BaseApi = "http://localhost:5000";
        opt.Name = "userCaller"; // 当前Caller的别名。非必填(仅有一个Caller时)，其Name不能重复
        opt.IsDefault = false; // 不是默认的Caller,默认的Caller支持注入ICallerProvider获取
    });

    #endregion

    #region 使用委托处理程序

    // option.UseHttpClient(opt =>
    // {
    //     opt.BaseApi = "http://localhost:5000";
    //     opt.Name = "https";
    //     opt.IsDefault = false;
    //     opt.Configure = client =>
    //     {
    //         client.Timeout = TimeSpan.FromSeconds(10); //10秒超时
    //     };
    // }).AddHttpMessageHandler<HttpClientDelegatingHandler>();

    #endregion

});
var app = builder.Build();

app.MapGet("/", () => "Hello HttpClientWeb.V1!");

app.MapGet("/Test/User/Get", async ([FromServices] ICallerProvider callerProvider) =>
{
    var user = await callerProvider.GetAsync<object, UserDto>("User", new { id = new Random().Next(1, 10) });
    return $"获取用户信息成功：用户名称为：{user!.Name}";
});

app.MapGet("/Test/User/Add", async ([FromServices] ICallerProvider callerProvider) =>
{
    string timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds.ToString(CultureInfo.InvariantCulture);
    var userName = "ss_" + timeSpan; //模拟一个随机的用户名
    string? response= await callerProvider.PostAsync<object, string>("User", new { Name = userName });
    return $"创建用户成功了，{response}";
});

app.Run();

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

using Masa.Utils.Caller.Core;
using Masa.Utils.Caller.DaprClient;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCaller(options =>
{
    options.UseDapr(masaDaprClientBuilder =>
    {
        masaDaprClientBuilder.AppId = "Assignment-Server";//设置当前caller下Dapr的AppId
    });
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
    var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
    string timeSpan = dateTimeOffset.ToUnixTimeSeconds().ToString();
    var userName = "ss_" + timeSpan; //模拟一个用户名
    string? response = await callerProvider.PostAsync<object, string>("User", new { Name = userName });
    return $"创建用户成功了，用户名称为：{response}";
});
app.MapGet("/Test/User/Hello", ([FromServices] ICallerProvider userCallerProvider, string name)
    => userCallerProvider.GetAsync<string>($"/Hello/{name}"));
app.Run();

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

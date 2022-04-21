var builder = WebApplication.CreateBuilder(args);

#region 非必须 (不使用HttpClientDelegatingHandler可不添加)

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<HttpClientDelegatingHandler>();

#endregion

builder.Services.AddCaller(option =>
{
    option.UseHttpClient(opt =>
    {
        opt.BaseApi = "http://localhost:5000";
        opt.Name = "http";
        opt.IsDefault = true; //是否默认的caller
    });
    option.UseHttpClient(opt =>
    {
        opt.BaseApi = "https://localhost:5001";
        opt.Name = "https";
        opt.IsDefault = false;
        opt.Configure = client =>
        {
            client.Timeout = TimeSpan.FromSeconds(10); //10秒超时
        };
    }).AddHttpMessageHandler<HttpClientDelegatingHandler>();
});
var app = builder.Build();

app.MapGet("/", () => "Hello HttpClientWeb.V1!");

app.MapGet("/Test/Hello1", ([FromServices] ICallerProvider callerProvider) 
    => callerProvider.GetAsync<string>("Hello1"));

app.MapGet("/Test/Hello2", ([FromServices] ICallerFactory callerFactory) =>
{
    var callerProvider = callerFactory.CreateClient("https");
    return callerProvider.PostAsync<object, string>("Hello2", new { });
});

app.Run();

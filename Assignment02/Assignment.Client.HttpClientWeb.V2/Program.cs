var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCaller();

#region 非必须 (不使用HttpClientDelegatingHandler可不添加)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<HttpClientDelegatingHandler>();
#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello HttpClientWeb.V2!");

app.MapGet("/Test/Hello1", ([FromServices] HttpCaller httpCaller) 
    => httpCaller.GetHello1Async());

app.MapGet("/Test/Hello2", ([FromServices] HttpsCaller httpsCaller) 
    => httpsCaller.GetHello2Async());

app.Run();

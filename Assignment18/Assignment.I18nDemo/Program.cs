using Assignment.I18nDemo.Resources;
using Masa.BuildingBlocks.Globalization.I18n;

var builder = WebApplication.CreateBuilder(args);

#region 自定义资源

builder.Services.Configure<MasaI18nOptions>(options =>
{
    options.Resources
        .Add<CustomResource>()
        .AddJson(Path.Combine("Resources", "I18n2"), new List<CultureModel>()
        {
            new("zh-CN"),
            new("en-US")
        });
});

#endregion

builder.Services.AddI18nByEmbedded(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseI18n();

app.MapGet("/", () => "Hello World!");

app.Map("/test", (string key) => I18n.T(key));

app.Map("/test2", (string key, II18n<CustomResource> i18n) => i18n.T(key));

app.Run();

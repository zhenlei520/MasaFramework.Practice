using Assignment.MasaConfiguration.Extensions.Options;
using Masa.BuildingBlocks.Configuration;
using Masa.Contrib.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var app = builder.AddMasaConfiguration(configurationBuilder =>
{
    // configurationBuilder.UseDcc();
    configurationBuilder.UseMasaOptions(options
        => options.MappingConfigurationApi<AppConfig>("AppId")); //映射远程节点下指定AppId的配置，其中配置对象名为AppConfig
}).Build();

app.MapGet("/", () => "Hello MasaConfiguration!");

// 推荐使用
app.MapGet("/AppConfig", (IOptions<AppConfig> appConfig)
    => appConfig.Value.ConnectionStrings.DefaultConnection);

// 使用MasaConfiguration获取配置
app.MapGet("/AppConfig/ConnectionStrings/MasaConfiguration", (IMasaConfiguration configuration)
    => configuration.Local["AppConfig:ConnectionStrings:DefaultConnection"]);

// 使用原始IConfiguration获取配置
app.MapGet("/AppConfig/ConnectionStrings/Default", (IConfiguration configuration)
    => configuration["Local:AppConfig:ConnectionStrings:DefaultConnection"]);

// 仅支持获取远程配置能力
app.MapGet("/AppConfig/ConnectionStrings/ByClient", async (IConfigurationApiClient client)
    => await client.GetDynamicAsync("Replace your environment", "Replace your cluster", "Replace your appId", "Replace your configObject", null));

app.Run();

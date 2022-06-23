using System.Linq.Expressions;
using System.Reflection;
using Masa.BuildingBlocks.Configuration;
using Masa.Contrib.Configuration;

namespace Assignment.MasaConfiguration.Extensions.Options;

public class AppConfig : LocalMasaConfigurationOptions
{
    /// <summary>
    /// 默认读取根节点下的《AppConfig》节点（默认节点名与类名一致，无需重载Section）
    /// 若当前配置不是根节点或者节点名与类名不一致，则需重载Section并重新赋值，多级节点以:分割
    /// </summary>
    // public override string? Section => null;

    public ConnectionStrings ConnectionStrings { get; set; }
}

public static class MasaConfigurationExtensions
{
    public static IMasaConfigurationBuilder UseDcc(this IMasaConfigurationBuilder builder)
    {

        return builder;
    }
}

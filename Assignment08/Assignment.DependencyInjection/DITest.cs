using Assignment.DependencyInjection.Infrastructure.Services;

namespace Assignment.DependencyInjection;

[TestClass]
public class DITest
{
    private IServiceCollection _services = default!;

    [TestInitialize]
    public void Init()
    {
        _services = new ServiceCollection();
        _services.AddAutoInject();
    }

    [TestMethod]
    public void TestStorageOptionsLifetimeEqualTransient()
    {
        Assert.IsTrue(_services.Any<StorageOptions>(ServiceLifetime.Transient));
    }

    [TestMethod]
    public void TestIDataReturnServicesCountIs2()
    {
        var data = ServiceProvider.GetServices<IData>().ToList();
        Assert.IsTrue(data.Count == 2); //判断IData被注册2次，分别是MemoryDataBase、SqliteDataBase，且生命周期为Scoped
        Assert.IsTrue(_services.Any<IData, MemoryDataBase>(ServiceLifetime.Scoped));
        Assert.IsTrue(_services.Any<IData, SqliteDataBase>(ServiceLifetime.Scoped));
    }

    [TestMethod]
    public void TestIEncryptionServiceReturnServiceCountIs1()
    {
        var encryptionServices = ServiceProvider.GetServices<IEncryptionService>().ToList(); //判断IEncryptionService被注册1次，生命周期为Singleton
        Assert.IsTrue(encryptionServices.Count == 1);
        Assert.IsTrue(_services.Any<IEncryptionService, Sha1EncryptionService>(ServiceLifetime.Singleton));
        Assert.IsTrue(encryptionServices[0].MethodName == "Sha1");
    }

    [TestMethod]
    public void TestICacheReturnCacheIsMemoryCache()
    {
        var caches = ServiceProvider.GetServices<ICache>().ToList();
        Assert.IsTrue(caches.Count == 1);
        Assert.IsTrue(caches[0] is MemoryCache);

        caches[0].Set("ke", "12");
    }

    [TestMethod]
    public void TestIgnoreInjection()
    {
        Assert.IsTrue(_services.Any<BaseService>(ServiceLifetime.Singleton));
        Assert.IsFalse(_services.Any<GoodsBaseService>(ServiceLifetime.Singleton));
        Assert.IsTrue(_services.Any<GoodsService>(ServiceLifetime.Singleton));
    }

    private IServiceProvider ServiceProvider => _services.BuildServiceProvider();
}

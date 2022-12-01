
在`Assignment17.Ordering.Infrastructure`文件夹下执行迁移命令

* 执行迁移命令

``` powershell
dotnet ef migrations add init --startup-project ../Assignment17.Ordering.API
```

* 数据库更新

``` powershell
dotnet ef database update --startup-project ../Assignment17.Ordering.API
```
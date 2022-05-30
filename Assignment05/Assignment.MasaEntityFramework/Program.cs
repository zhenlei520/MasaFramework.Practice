using System.Reflection;
using Assignment.MasaEntityFramework.Infrastructure;
using Assignment.MasaEntityFramework.Infrastructure.Extensions;
using Assignment.MasaEntityFramework.Models;
using Assignment.MasaEntityFramework.Requesties;
using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Data.Contracts.DataFiltering;
using Masa.Contrib.Data.Contracts.EF;
using Masa.Contrib.Data.EntityFrameworkCore;
using Masa.Contrib.Data.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

#region swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region 添加用户数Db上下文

#region 原始写法

// builder.Services.AddMasaDbContext<UserDbContext>(options =>
// {
//     options.Builder = (_, dbContextOptionsBuilder) => dbContextOptionsBuilder.UseInMemoryDatabase("test");
//     options.UseFilter(); //启用数据过滤，完整写法：options.UseFilter(filterOptions => filterOptions.EnableSoftDelete = true);
// });

#endregion

#region 从配置文件中获取

// builder.Services.Configure<MasaDbConnectionOptions>(option =>
// {
//     option.ConnectionStrings = new ConnectionStrings(new List<KeyValuePair<string, string>>()
//     {
//         new("User", "test2")
//     });
// });
builder.Services.AddSingleton<IConnectionStringProvider,CustomizeConnectionStringProvider>();
builder.Services.AddSingleton<IDbConnectionStringProvider,CustomizeDbConnectionStringProvider>();

builder.Services.AddMasaDbContext<UserDbContext>(options => options.UseInMemoryDatabase().UseFilter());

#endregion

#endregion

var app = builder.Build();

#region swagger and 迁移数据库

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    #region 迁移数据库

    app.MigrateDbContext<UserDbContext>((context, services) =>
    {
    });

    #endregion
}

#endregion

app.MapGet("/", () => "Hello World!");

app.MapPost("/add", (UserDbContext dbContext, [FromBody] AddUserRequest request) =>
{
    dbContext.Set<User>().Add(new User()
    {
        Name = request.Name,
        Gender = request.Gender,
        BirthDay = request.BirthDay
    });
    dbContext.SaveChanges();
});

app.MapDelete("/delete", (UserDbContext dbContext, int id) =>
{
    var user = dbContext.Set<User>().First(u => u.Id == id);
    dbContext.Set<User>().Remove(user);
    dbContext.SaveChanges();
});


app.MapGet("/list", (UserDbContext dbContext) =>
{
    return dbContext.Set<User>().ToList();
});

app.MapGet("/all", (UserDbContext dbContext, [FromServices] IDataFilter dataFilter) =>
{
    using (dataFilter.Disable<ISoftDelete>())
    {
        return dbContext.Set<User>().ToList();
    }
});

app.MapGet("/database", (UserDbContext dbContext) =>
{
    var field = typeof(MasaDbContext).GetField("Options", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)!;
    var masaDbContextOptions = field.GetValue(dbContext) as MasaDbContextOptions;
    foreach (var dbContextOptionsExtension in masaDbContextOptions!.Extensions)
    {
        if (dbContextOptionsExtension is InMemoryOptionsExtension memoryOptionsExtension)
        {
            return memoryOptionsExtension.StoreName;
        }
    }

    return "";
});

app.Run();

using Assignment.MasaEntityFramework.Infrastructure;
using Assignment.MasaEntityFramework.Infrastructure.Extensions;
using Assignment.MasaEntityFramework.Models;
using Assignment.MasaEntityFramework.Requesties;
using Masa.BuildingBlocks.Data.Contracts.DataFiltering;
using Masa.Contrib.Data.Contracts.EF;
using Masa.Contrib.Data.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

builder.Services.AddMasaDbContext<UserDbContext>(options =>
{
    options.Builder = (_, dbContextOptionsBuilder) => dbContextOptionsBuilder.UseInMemoryDatabase("test");
    options.UseFilter(); //启用数据过滤，完整写法：options.UseFilter(filterOptions => filterOptions.EnableSoftDelete = true);
});

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

app.Run();

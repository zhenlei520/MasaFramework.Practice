// See https://aka.ms/new-console-template for more information

using Assignment.Mapster.Domain;
using Assignment.Mapster.Domain.Aggregate;
using Assignment.Mapster.Dto;
using Assignment.Mapster.Infrastructure;
using Mapster;

Console.WriteLine("Hello Mapster!");

#region 匿名对象映射到UserDto

var user = new
{
    Id = 1,
    Name = "Tom",
    Gender = 1,
    BirthDay = DateTime.Parse("2002-01-01")
};
var userDto = user.Adapt<UserDto>();

#endregion

#region 数据类型转换

#region 基本类型

decimal i = 123.Adapt<decimal>(); //equal to (decimal)123;

#endregion

#region 枚举类型

var fileMode = "Create, Open".Adapt<FileMode>();

#endregion

#endregion

#region Queryable Extensions

using (var dbContext = new UserDbContext())
{
    dbContext.Database.EnsureCreated();

    dbContext.User.Add(new User()
    {
        Id = 1,
        Name = "Tom",
        Gender = 1,
        BirthDay = DateTime.Parse("2002-01-01")
    });
    dbContext.SaveChanges();

    var userItemList = dbContext.User.ProjectToType<UserDto>().ToList();
}

#endregion

Console.ReadKey();

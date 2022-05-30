using Assignment.MasaEntityFramework.Models;
using Masa.BuildingBlocks.Data;
using Masa.Contrib.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment.MasaEntityFramework.Infrastructure;

[ConnectionStringName("User")]//默认读取节点: DefaultConnection，增加特性后可自定义读取节点
public class UserDbContext : MasaDbContext//重点：改为继承MasaDbContext，当存在多个DbContext时，继承MasaDbContext<TDbContext>
{
    public DbSet<User> User { get; set; }

    public UserDbContext(MasaDbContextOptions options) : base(options)//当存在多个DbContext时，需要改为MasaDbContextOptions<TDbContext> options
    {
    }
}

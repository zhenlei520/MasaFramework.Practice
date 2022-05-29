using Masa.BuildingBlocks.Data.Contracts.DataFiltering;

namespace Assignment.MasaEntityFramework.Models;

public class User : ISoftDelete//重点：改为实现ISoftDelete
{
    public int Id { get; set; }

    public string Name { get; set; }

    public uint Gender { get; set; }

    public DateTime BirthDay { get; set; }

    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 重点: 需要提供set支持，可以是private
    /// 删除为true，否则为false
    /// </summary>
    public bool IsDeleted { get; private set; }

    public User()
    {
        this.CreationTime = DateTime.Now;
    }
}

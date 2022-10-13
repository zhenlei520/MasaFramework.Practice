using Assignment.MinimalApiDemo.Infrastructure;
using Assignment.MinimalApiDemo.Request;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MinimalApiDemo.Services;

// public class UserService : ServiceBase
// {
//     public void Add(RegisterUserRequest request, IData data)
//     {
//         data.Add(request.Name, request.Age);
//     }
// }

public class UserService : ServiceBase
{
    public UserService()
    {

        App.MapGet("/map", (Point point) => $"Point: {point.X}, {point.Y}");
    }



    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static bool TryParse(string? value, IFormatProvider? provider,
            out Point? point)
        {
            // Format is "(12.3,10.1)"
            var trimmedValue = value?.TrimStart('(').TrimEnd(')');
            var segments = trimmedValue?.Split(',',
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (segments?.Length == 2
                && double.TryParse(segments[0], out var x)
                && double.TryParse(segments[1], out var y))
            {
                point = new Point { X = x, Y = y };
                return true;
            }

            point = null;
            return false;
        }
    }

    public void Add(RegisterUserRequest request, IData data)
    {
        data.Add(request.Name, request.Age);
    }

    /// <summary>
    /// Get: /api/v1/users
    /// </summary>
    [IgnoreRoute]
    public IResult Get2Async(UserQuery query)
    {
        //todo: 获取用户详情
        return Results.Ok();
    }

    public class UserQuery
    {
        public string Name { get; set; }
    }

    // /// <summary>
    // /// Get: /api/v1/users/{id}
    // /// </summary>
    // public Task<IResult> GetAsync(Guid id)
    // {
    //     // todo: 查询用户信息
    //     var user = new User()
    //     {
    //     };
    //     return Task.FromResult(Results.Ok(user));
    // }
    //
    // public class User
    // {
    //
    // }
    //
    // /// <summary>
    // /// Post：/user/add
    // /// </summary>
    // [RoutePattern(pattern: "user/add")]
    // public Task<IResult> Add(AddUserRequest request)
    // {
    //     //todo: 添加用户
    //     return Task.FromResult(Results.Accepted());
    // }
    //
    // public class AddUserRequest
    // {
    //
    // }
    //
    // [IgnoreRoute]
    // public bool ExistUser(string name)
    // {
    //     //检查用户是否存在
    //     return false;
    // }
    //
    // /// <summary>
    // /// Post：/api/v1/users/audit
    // /// </summary>
    // [RoutePattern(HttpMethod = "Post")]
    // public IResult Audit(Guid id, AuditUserRequest request, [FromServices] IUserRepository userRepository)
    // {
    //     //todo: 审核用户信息
    //     return Results.Ok();
    // }
    //
    // public class AuditUserRequest
    // {
    //
    // }
    //
    // public interface IUserRepository
    // {
    //
    // }
    //
    // /// <summary>
    // /// Post：/api/v1/users/audit
    // /// </summary>
    // [RoutePattern(HttpMethod = "Post")]
    // public IResult Audit(Guid id, [FromBody] AuditUserRequest request)
    // {
    //     //todo: 审核用户信息
    //     return Results.Ok();
    // }
}

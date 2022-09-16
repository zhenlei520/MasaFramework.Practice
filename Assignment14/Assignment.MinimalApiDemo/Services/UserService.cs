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

public class User2Service : ServiceBase
{
    public void Add([FromBody] RegisterUserRequest request, IData data)
    {
        data.Add(request.Name, request.Age);
    }
}

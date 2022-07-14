using Assignment.MinimalApiDemo.Infrastructure;
using Assignment.MinimalApiDemo.Request;
using Masa.Contrib.Service.MinimalAPIs;

namespace Assignment.MinimalApiDemo.Services;

// public class UserService : ServiceBase
// {
//     public UserService(IServiceCollection services) : base(services, "user")
//     {
//         MapPost(Register, "register");
//     }
//
//     private void Register(RegisterUserRequest request, IData data)
//     {
//         data.Add(request.Name, request.Age);
//     }
// }

public class UserService : ServiceBase
{
    public UserService(IServiceCollection services) : base(services)
    {
        App.MapPost("/user/register", Register);
    }

    private void Register(RegisterUserRequest request, IData data)
    {
        data.Add(request.Name, request.Age);
    }
}

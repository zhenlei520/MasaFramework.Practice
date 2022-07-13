using Assignment.MinimalApiDemo.Infrastructure;
using Assignment.MinimalApiDemo.Request;
using Assignment.MinimalApiDemo.Response;
using Masa.Contrib.Service.MinimalAPIs;

namespace Assignment.MinimalApiDemo.Services;

public class UserService : ServiceBase
{
    public UserService(IServiceCollection services) : base(services, "user")
    {
        MapGet(GetList, "list");
        MapPost(Register, "register");
    }

    private List<UserItemResponse> GetList()
    {
        return new()
        {
            new UserItemResponse(1, "John Doe", 18),
            new UserItemResponse(2, "Jim", 20)
        };
    }

    private void Register(RegisterUserRequest request, IData data)
    {
        data.Add(request.Name, request.Age);
    }
}

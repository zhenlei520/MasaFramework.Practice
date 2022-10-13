using Assignment.GlobalExceptionDemo;
using Assignment.GlobalExceptionDemo.Model;
using Masa.Utils.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMasaExceptionHandler, ExceptionHandler>();

var app = builder.Build();

app.UseMasaExceptionHandler(options =>
{
    // options.UseExceptionHanlder<ExceptionHandler>();
    options.ExceptionHandler = context =>
    {
        if (context.Exception is ArgumentNullException ex)
        {
            context.ToResult($"{ex.ParamName}不能为空");
        }
    };
});

app.MapGet("/", () => "Hello Assignment.GlobalExceptionDemo!");

app.MapPost("/register", (User user) =>
{
    if (string.IsNullOrEmpty(user.Name))
        throw new ArgumentNullException(nameof(user.Name));

    //todo: Impersonate a registered user
});

app.Run();

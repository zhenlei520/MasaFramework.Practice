using Assignment.GlobalExceptionDemo;
using Assignment.GlobalExceptionDemo.Model;
using Assignment.GlobalExceptionDemo.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Masa.Utils.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMasaExceptionHandler, ExceptionHandler>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<User>());

var app = builder.Build();

app.UseMasaExceptionHandler(options =>
{
    options.UseExceptionHanlder<ExceptionHandler>();
    // options.ExceptionHandler = context =>
    // {
    //     if (context.Exception is ValidationException ex)
    //     {
    //         string message = ex.Errors.Select(error => error.ErrorMessage).FirstOrDefault()!;
    //         context.ToResult(message);
    //     }
    // };
});

app.MapGet("/", () => "Hello Assignment.GlobalExceptionDemo!");

app.MapPost("/register", (User user) =>
{
    var validator = new UserValidator();
    var results = validator.Validate(user);
    if (!results.IsValid)
        throw new ValidationException(results.Errors);

    //todo: Impersonate a registered user
});

app.Run();

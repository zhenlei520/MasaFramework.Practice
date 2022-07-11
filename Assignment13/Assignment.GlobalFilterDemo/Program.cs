using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services
    .AddMvc()
    .AddFluentValidation()
    .AddMasaExceptionHandler(options =>
    {
        options.ExceptionHandler = context =>
        {
            if (context.Exception is ValidationException ex)
            {
                string message = ex.Errors.Select(error => error.ErrorMessage).FirstOrDefault()!;
                context.ToResult(message);
            }
        };
    });

var app = builder.Build();

app.MapGet("/", () => "Hello Assignment.GlobalFilterDemo3!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

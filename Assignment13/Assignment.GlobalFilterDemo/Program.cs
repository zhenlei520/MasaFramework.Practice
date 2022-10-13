using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services
    .AddMvc()
    .AddMasaExceptionHandler(options =>
    {
        options.ExceptionHandler = context =>
        {
            if (context.Exception is ArgumentNullException ex)
            {
                context.ToResult($"{ex.ParamName}不能为空");
            }
        };
    });

var app = builder.Build();

app.MapGet("/", () => "Hello Assignment.GlobalFilterDemo3!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

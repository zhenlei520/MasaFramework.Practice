using Assignment.OSS.Request;
using Masa.BuildingBlocks.Storage.ObjectStorage;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAliyunStorage();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/upload", async (HttpRequest request, IClient client) =>
{
    var form = await request.ReadFormAsync();
    var formFile = form.Files["file"];
    if (formFile == null)
        throw new FileNotFoundException("Can't upload empty file");

    await client.PutObjectAsync("storage1-test", formFile.FileName, formFile.OpenReadStream());
});

app.MapDelete("/delete", async (IClient client, [FromBody] DeleteRequest request) =>
{
    await client.DeleteObjectAsync("storage1-test", request.Key);
});

app.MapGet("/exist", async (IClient client, string key) =>
{
    await client.ObjectExistsAsync("storage1-test", key);
});

app.MapGet("/download", async (IClient client, string key, string path) =>
{
    await client.GetObjectAsync("storage1-test", key, stream =>
    {
        using var requestStream = stream;
        byte[] buf = new byte[1024];
        var fs = File.Open(path, FileMode.OpenOrCreate);
        int len;
        while ((len = requestStream.Read(buf, 0, 1024)) != 0)
        {
            fs.Write(buf, 0, len);
        }
        fs.Close();
    });
});

app.MapGet("/GetSts", (IClient client) =>
{
    client.GetSecurityToken();
});

app.MapGet("/GetToken", (IClient client) =>
{
    client.GetToken();
});

app.Run();

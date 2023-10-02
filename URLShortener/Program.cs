using Microsoft.AspNetCore.Mvc;
using URLShortener.BLL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


app.MapGet("/{url}", (string url) =>
{
    try
    {
        UrlBLL urlBLL = new UrlBLL();

        var s = urlBLL.GetUrl(url);

        return Results.Redirect(s, true, true);
    }
    catch (Exception)
    {
        return Results.BadRequest("Internal Server Error!");
    }
});

app.MapPost("/", ([FromBody] string longUrl) =>
{
    try
    {
        UrlBLL urlBLL = new UrlBLL();

        urlBLL.SetUrl(longUrl);

        return Results.Ok("Url Added Successfully");
    }
    catch (Exception)
    {
        return Results.BadRequest("Internal Server Error!");
    }
});

app.Run();
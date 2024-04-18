var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{*path}", (HttpContext context) =>
{
    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    return context.Response.SendFileAsync(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "index.html"));
});
app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();
    

app.Run();

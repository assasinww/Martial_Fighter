using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Configure MIME types
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream",
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/your-unity-files-path" // Adjust this path according to your Unity WebGL files' location
});

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

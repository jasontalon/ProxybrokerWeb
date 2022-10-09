using Microsoft.Extensions.FileProviders;

string? ResolveContentRootPath(WebApplication app)
{
    var maxAttempt = 4;
    var attempts = 0;
    do
    {
        var level = string.Join("/", Enumerable.Repeat("..", attempts).ToList());

        var _contentPath = Path.Combine(Directory.GetCurrentDirectory(), level, "wwwroot");

        _contentPath = Path.GetFullPath(_contentPath);
        app.Logger.LogInformation(_contentPath);

        if (Directory.Exists(_contentPath))
            return _contentPath;

        attempts++;
    } while (maxAttempt > attempts);

    return null;
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Logger.LogInformation("Web root path: " + app.Environment.WebRootPath);
app.Run();
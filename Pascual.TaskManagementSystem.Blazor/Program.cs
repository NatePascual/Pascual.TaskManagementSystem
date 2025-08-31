using Microsoft.AspNetCore.Builder;
using Pascual.TaskManagementSystem.Blazor.Components;
using Pascual.TaskManagementSystem.Blazor.Services;
using static Pascual.TaskManagementSystem.Blazor.Services.ToastMessage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri("http://localhost:5001/")
//});

//builder.Services.AddScoped<TaskApiService>();
builder.Services.AddScoped<ToastService>();

builder.Services.AddHttpClient<TaskApiService>(client =>
{
    var apiBase = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8080";
    client.BaseAddress = new Uri(apiBase);
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

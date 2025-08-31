using Microsoft.AspNetCore.Builder;
using Pascual.TaskManagementSystem.Blazor.Components;
using Pascual.TaskManagementSystem.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri("https://localhost:5001/") 
//});

//builder.Services.AddScoped<TaskApiService>();

builder.Services.AddHttpClient<TaskApiService>(client =>
{
    var apiBase=builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8080";
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

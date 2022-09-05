using Microsoft.AspNetCore.ResponseCompression;
using CharacterPlanner.Server.Services;
using CharacterPlanner.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ClientCredentials>(builder.Configuration.GetSection("BattleNet"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.ConfigureInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();

app.MapRazorPages();
app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();
app.MapFallbackToFile("index.html");

app.Run();

using CharacterPlanner.DependencyInjection;
using CharacterPlanner.Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ClientCredentials>(builder.Configuration.GetSection("BattleNet"));

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(config =>
{
    config.RootPath = "ClientApp/dist";
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.UseAuthentication();
app.MapFallbackToFile("index.html");

app.Run();

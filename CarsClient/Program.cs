using CarsClient.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using CarsClient.Services;
using CarsClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IIdentityServer4Service, IdentityServer4Service>();

builder.Services.AddSingleton<GlobalVariables>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = "cookie";
	options.DefaultChallengeScheme = "oidc";
})
	.AddCookie("cookie")
	.AddOpenIdConnect("oidc", options =>
	{
		options.Authority = builder.Configuration["IdentityServerAddress"];
		options.ClientId = "oidcMVCApp";
		options.ClientSecret = "CarsApi";

		options.ResponseType = "code";
		options.UsePkce = true;
		options.ResponseMode = "query";

		options.Scope.Add("carsApi.read");
		options.SaveTokens = true;
	});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error?statusCode={0}");
app.UseCustomExceptionHandler();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

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
		options.Authority = "https://localhost:5001";
		options.ClientId = "oidcMVCApp";
		options.ClientSecret = "CarsApi";

		options.ResponseType = "code";
		options.UsePkce = true;
		options.ResponseMode = "query";

		options.Scope.Add("carsApi.read");
		options.SaveTokens = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

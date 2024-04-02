using CarsClient.Middleware;
using CarsClient;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Host.UseSerilog((ctx, lc) =>
        lc
            .MinimumLevel.Error()
            .WriteTo.File($"logs/CarsWebAppLog-.log", rollingInterval:
                RollingInterval.Day));
}
else
{
    builder.Host.UseSerilog((ctx, lc) =>
        lc
            .WriteTo.Console());
}

// Add services to the container.
builder.Services.AddControllersWithViews();

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

        options.RequireHttpsMetadata = false;
    });

if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://*:5000");
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseCustomExceptionHandler();
app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

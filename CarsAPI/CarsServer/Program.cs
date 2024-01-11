using Applicaton;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using Persistence;
using Persistence.Data;
using System.Reflection;
using MediatR;
using CarsServer.Middleware;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => 
                lc
				.MinimumLevel.Error()
				.WriteTo.File($"logs/CarsWebAppLog-.log", rollingInterval:
                    RollingInterval.Day));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

 
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddMediatR(typeof(DataContext).Assembly);

builder.Services.AddAutoMapper(config =>
{
    var service = builder.Services.BuildServiceProvider().GetService<IRepositoryManager>();

    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly(), service));
    config.AddProfile(new AssemblyMappingProfile(typeof(IDataContext).Assembly, service));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddAuthentication("Bearer")
	.AddIdentityServerAuthentication("Bearer", options =>
	{
		options.ApiName = "carsApi";
		options.Authority = builder.Configuration["IdentityServerAddress"];
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("carsApiScope", policy =>
	{
		policy.RequireAuthenticatedUser();
		policy.RequireClaim("scope", "carsApi.read");
	});
});

if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://*:7052");
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<DataContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        Log.Fatal(exception, "An error occurred while db initialization");
    }
}


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UserCustomExceptionHandler();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

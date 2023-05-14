using Applicaton;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using Persistence;
using Persistence.Data;
using System.Reflection;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using Persistence.Repository;
using CarsServer.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog.Events;
using Serilog;
/*
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File($"logs/CarsWebAppLog-.log", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();*/

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => 
                lc
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File($"logs/CarsWebAppLog-.log", rollingInterval:
                    RollingInterval.Day));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["IdentityServerAddress"];
        options.Audience = "CarsWebAPI";
        options.RequireHttpsMetadata = false;
    });

//builder.Services.AddScoped<IDataContext, DataContext>();

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
        Log.Fatal(exception, "An error occurred while app initialization");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

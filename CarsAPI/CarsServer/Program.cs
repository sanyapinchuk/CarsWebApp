using Applicaton;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using Persistence;
using Persistence.Data;
using System.Reflection;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddMediatR(typeof(DataContext).Assembly);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly(), builder.Services.BuildServiceProvider().GetService<IRepositoryManager>()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IDataContext).Assembly, builder.Services.BuildServiceProvider().GetService<IRepositoryManager>()));
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
       // Log.Fatal(exception, "An error occurred while app initialization");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();
//app.UseAuthorization();

app.MapControllers();

app.Run();

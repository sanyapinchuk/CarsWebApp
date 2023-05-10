using Applicaton;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using Persistence;
using Persistence.Data;
using System.Reflection;
using MediatR;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMediatR(typeof(DataContext).Assembly);

builder.Services.AddAutoMapper(config =>
{
   /* var dataContext2 = builder.Services
    .Where(s => s.ImplementationType == typeof(DataContext));

    var dataContext3 = dataContext2.First();
    var dataContext = dataContext3.ImplementationInstance as DataContext;
     config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly(),dataContext));
     config.AddProfile(new AssemblyMappingProfile(typeof(IDataContext).Assembly, dataContext));
    */
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IDataContext).Assembly));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

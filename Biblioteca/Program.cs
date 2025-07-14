using BibliotecaDAL.Context;
using BibliotecaDAL.IRepositories;
using BibliotecaDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BibliotecaBLL.Serivices;
using BibliotecaBLL.IServices;
using Biblioteca.AutoMapperProfiles;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Database
builder.Services.AddDbContext<BibliotecaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IPublisherService, PublisherSerivice>();

// Unit Of Work Pattern
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


using Domain.Interface;
using Infrastructure.Data;//Para que aceda al contexto. 
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Infrastructure.Data;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyeciones
//BD
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
builder.Configuration["ConnectionStrings:EcommerceDBConnectionString"], b => b.MigrationsAssembly("Infrastructure")));

//Respository
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();

//Services
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ISneakerServices, SneakerServices>();

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

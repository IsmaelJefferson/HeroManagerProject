using HeroManager.API.Extensions;
using HeroManager.Application.Interface;
using HeroManager.Application.Mappings;
using HeroManager.Application.Services;
using HeroManager.Domain.Interfaces;
using HeroManager.Infra.Persistence;
using HeroManager.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerDocs();

builder.Services.AddCorsPolicy();

builder.Services.AddAutoMapper(typeof(Mappingprofile));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("HeroManagerDb"));

builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<ISuperPowerRepository, SuperPowerRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
using DeliveryApp.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RoleService.Application.Commands.Permisos.Handler;
using RoleService.Application.Interfaces;
using RoleService.Infrastructure.Data;
using RoleService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("RoleService.API")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePermisoHandler).Assembly));

builder.Services.AddSingleton<Argon2Hasher>();

builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "DeliveryApp API",
        Version = "v1",
        Description = "API para gestionar Roles, permisos y asignaciones en DeliveryApp.",
        Contact = new OpenApiContact
        {
            Name = "Gustavo Fernández",
            Email = "soygustavofernandez@gmail.com",
            Url = new Uri("https://github.com/soygustavofernandez")
        },
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoleService v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using Microsoft.OpenApi.Models;
using DotNetEnv;
using GestionAcademicaAPI.Controllers;
using GestionAcademicaAPI.Repositories.Implementations;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Implementations;
using GestionAcademicaAPI.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde el archivo .env
Env.Load();

// Agregar controladores al contenedor de servicios (para Web API)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Agregar soporte para Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GestionAcademiaAPI",
        Version = "v1",
        Description = "Una API para aprobar DRI07 ASP.NET Core",
        Contact = new OpenApiContact
        {
            Name = "UPIS",
            Email = "escom_upis@ipn.mx"
        }
    });

    // Habilitar la generación de comentarios XML para los modelos
    var xmlFile = Path.Combine(Directory.GetCurrentDirectory(), "GestionAcademica.xml");
    c.IncludeXmlComments(xmlFile);
});

// Agregar servicios al contenedor.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Registrar los repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();

// Registrar los servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();

// Registrar los controladores
builder.Services.AddScoped<UsuarioController>();
builder.Services.AddScoped<AdministradorController>();

// Configurar CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionAcademiaAPI v1"));
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();

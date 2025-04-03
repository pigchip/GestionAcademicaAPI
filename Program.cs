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
using GestionAcademicaAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000", "https://0.0.0.0:5001");

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
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IEscuelaRepository, EscuelaRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IPropuestaRepository, PropuestaRepository>();
builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<IPropuestaMateriaRepository, PropuestaMateriaRepository>();

// Registrar los servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IEscuelaService, EscuelaService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IPropuestaService, PropuestaService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<IPropuestaMateriaService, PropuestaMateriaService>();

// Registrar los controladores
builder.Services.AddScoped<UsuarioController>();
builder.Services.AddScoped<AdministradorController>();
builder.Services.AddScoped<EstudianteController>();
builder.Services.AddScoped<EscuelaController>();
builder.Services.AddScoped<MateriaController>();
builder.Services.AddScoped<PropuestaController>();
builder.Services.AddScoped<SolicitudController>();
builder.Services.AddScoped<ComentarioController>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionAcademiaAPI v1"));
app.UseDeveloperExceptionPage();
app.UseRouting();

// Custom CORS middleware
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", context.Request.Headers["Origin"]);
    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 204;
        return;
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

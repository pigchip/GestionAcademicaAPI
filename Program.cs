using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using Microsoft.OpenApi.Models;
using DotNetEnv;
using GestionAcademicaAPI.Controllers;
using GestionAcademicaAPI.Repositories.Implementations;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Implementations;
using GestionAcademicaAPI.Services.Interfaces;
using GestionAcademicaAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5001", "https://0.0.0.0:8000");

// Cargar variables de entorno desde el archivo .env
Env.Load();

// Configure CORS properly
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://www.escom.ipn.mx")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});


// Agregar controladores al contenedor de servicios (para Web API)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Eliminar ReferenceHandler.Preserve para evitar $id
        options.JsonSerializerOptions.ReferenceHandler = null; // O usa ReferenceHandler.IgnoreCycles si quieres manejar ciclos sin $id
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener nombres como están en el DTO
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

    var xmlFile = Path.Combine(Directory.GetCurrentDirectory(), "GestionAcademica.xml");
    c.IncludeXmlComments(xmlFile);
});

// Registrar servicios
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

// Registrar los servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IEscuelaService, EscuelaService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IPropuestaService, PropuestaService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

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

// Use the built-in CORS middleware
app.UseCors();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
using ClinicPro.Application.Features.Doctors.Commands.CreateDoctor;
using ClinicPro.Application.Features.Doctors.Queries.GetDoctors;
using ClinicPro.Application.Interfaces;
using ClinicPro.Application.Mapper;
using ClinicPro.Application.Services;
using ClinicPro.Core.Interfaces;
using ClinicPro.Infrastructure.Middleware;
using ClinicPro.Infrastructure.Persistence;
using ClinicPro.Infrastructure.Persistence.MySQLConn;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using System.Text;
// using AspNetCoreRateLimit; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CreateDoctorCommand).Assembly);
    config.RegisterServicesFromAssembly(typeof(GetDoctorsQuery).Assembly);
});

//Swagger
// Configura Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClinicPro_Backend", Version = "v1" });

    // Configuración de autenticación con Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT en el formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var serviceName = "MiWebAPI";
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()  // 📡 Captura llamadas HTTP
            .AddHttpClientInstrumentation()  // 📡 Captura peticiones HTTP salientes
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4317"); // Endpoint de OpenTelemetry Collector
            });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation() // 📊 Métricas de ASP.NET Core
            .AddHttpClientInstrumentation(); // 📊 Métricas de llamadas HTTP
    });

// Rate Limiting > NET 7
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("fixed", limiterOptions =>
//    {
//        limiterOptions.PermitLimit = 2;
//        limiterOptions.Window = TimeSpan.FromMinutes(1);
//    });
//});

//Rate Limiting < NET 7
//builder.Services.AddMemoryCache();
//builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
//builder.Services.AddInMemoryRateLimiting();
//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


// Application
builder.Services.AddScoped<IAuthService, AuthService>();

// Infrastructure
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Add MySQL
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection") ?? throw new InvalidOperationException("Connection string 'MySQLConnection' not found.");
builder.Services.AddScoped<MySQLDatabase>(_ => new MySQLDatabase(connectionString));

// Configurar autenticación con JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found.");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Configurar CORS para aceptar peticiones de cualquier lugar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClinicPro_Backend v1");
    c.RoutePrefix = "swagger"; // Cambia a "" si quieres que esté en http://localhost:8080/
});

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();

app.UseCors("AllowFrontend");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// < NET 7
//app.UseIpRateLimiting();

// > NET 7
//app.UseRateLimiter();
//app.MapControllers().RequireRateLimiting("fixed");

//app.MapControllers();

app.UseHttpMetrics();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapMetrics();
});

app.Run();

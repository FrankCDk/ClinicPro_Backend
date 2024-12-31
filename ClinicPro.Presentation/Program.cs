using ClinicPro.Application.Interfaces;
using ClinicPro.Application.Mapper;
using ClinicPro.Application.Services;
using ClinicPro.Application.Validations.Auth;
using ClinicPro.Core.Interfaces;
using ClinicPro.Infrastructure.Middleware;
using ClinicPro.Infrastructure.Persistence;
using ClinicPro.Infrastructure.Persistence.MySQLConn;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

// Application
builder.Services.AddScoped<IAuthService, AuthService>();

// Infrastructure
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

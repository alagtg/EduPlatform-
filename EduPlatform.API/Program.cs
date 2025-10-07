using System.Text;
using EduPlatform.API.Data;
using EduPlatform.API.Repositories.Implementations;
using EduPlatform.API.Repositories.Interfaces;
using EduPlatform.API.Services.Implementations;
using EduPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ======================================
// 🔹 1. CONFIGURATION BASE DE DONNÉES (SQL SERVER)
// ======================================
builder.Services.AddDbContext<EduDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ======================================
// 🔹 2. INJECTION DE DÉPENDANCES (DI)
// ======================================

// Repositories
builder.Services.AddScoped<IProfRepository, ProfRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProfService, ProfService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddHttpContextAccessor();

// ======================================
// 🔹 3. CONFIGURATION CONTROLLERS + SWAGGER
// ======================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Swagger avec Auth JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EduPlatform.API",
        Version = "v1",
        Description = "API d'une plateforme éducative (Professeurs / Étudiants)"
    });

    // 🔐 Définition de la sécurité JWT pour Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Saisis : 'Bearer' [espace] + ton token JWT.\n\nExemple : Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    // 🔐 Application de la sécurité JWT à toutes les routes protégées
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

// ======================================
// 🔹 4. CONFIGURATION JWT
// ======================================
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// ======================================
// 🔹 5. CONFIGURATION CORS POUR ANGULAR
// ======================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// ======================================
// 🔹 6. CONSTRUCTION DE L’APPLICATION
// ======================================
var app = builder.Build();

// 🔸 Middleware pour servir les fichiers (uploads)
app.UseStaticFiles();

// 🔸 Swagger disponible en Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔸 HTTPS
app.UseHttpsRedirection();

// 🔸 CORS (pour communication Angular ↔ .NET)
app.UseCors("AllowAngularApp");

// 🔸 Authentification & Autorisation
app.UseAuthentication();
app.UseAuthorization();

// 🔸 Mapping des contrôleurs
app.MapControllers();

// ======================================
// 🔹 7. LANCEMENT
// ======================================
app.Run();

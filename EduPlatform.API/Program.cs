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
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ======================================
// 🔹 1. CONFIGURATION BASE DE DONNÉES (SQL SERVER)
// ======================================
builder.Services.AddDbContext<EduDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ======================================
// 🔹 2. INJECTION DE DÉPENDANCES (DI)
// ======================================
builder.Services.AddScoped<IClassroomService, ClassroomService>();

// Repositories
builder.Services.AddScoped<IProfRepository, ProfRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProfService, ProfService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();

builder.Services.AddHttpContextAccessor();

// ======================================
// 🔹 3. CONFIGURATION CONTROLLERS + JSON + SWAGGER
// ======================================

// ✅ Ajoute les options JSON pour éviter les boucles entre entités EF
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

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

    // Authentification JWT dans Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Saisis : 'Bearer' [espace] + ton token JWT.\n\nExemple : Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
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
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// ======================================
// 🔹 6. CONSTRUCTION DE L’APPLICATION
// ======================================
var app = builder.Build();

// ✅ Vérifie et prépare les dossiers wwwroot / uploads / cahiers
var webRoot = app.Environment.WebRootPath;
if (string.IsNullOrEmpty(webRoot))
{
    webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    app.Environment.WebRootPath = webRoot;
}

if (!Directory.Exists(webRoot))
{
    Directory.CreateDirectory(webRoot);
    Console.WriteLine($"📁 Dossier créé : {webRoot}");
}

var uploadsPath = Path.Combine(webRoot, "uploads", "cahiers");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
    Console.WriteLine($"📁 Dossier créé : {uploadsPath}");
}
else
{
    Console.WriteLine($"📁 Dossier existant : {uploadsPath}");
}

// ======================================
// 🔹 7. MIDDLEWARE
// ======================================

// ✅ Accès public aux fichiers
app.UseStaticFiles();

// ✅ Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ HTTPS redirection
app.UseHttpsRedirection();

// ✅ CORS pour Angular
app.UseCors("AllowAngularApp");

// ✅ Authentification & autorisation
app.UseAuthentication();
app.UseAuthorization();

// ✅ Contrôleurs
app.MapControllers();

// ======================================
// 🔹 8. LANCEMENT
// ======================================
app.Run();

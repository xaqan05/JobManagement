using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using JobManagement.API.Services;
using JobManagement.Application.Interfaces;
using JobManagement.Domain.Entities;
using JobManagement.Persistence;
using JobManagement.Application;
using JobManagement.Persistence.Context;
using OfficeOpenXml;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

ExcelPackage.License.SetNonCommercialPersonal("Your Name");



builder.Services.AddMsSql(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddPasswordValidator<PasswordValidator<AppUser>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;
});

var jwtKey = builder.Configuration["Jwt:Key"] ?? "JobManagementSecretKeyForDevelopmentOnly12345";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "JobManagement";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "JobManagementUsers";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

builder.Services.AddRepos();
builder.Services.AddServices();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddAuthorization();







builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Job Management API",
        Version = "v1",
        Description = "API for managing users, companies, job seekers, vacancies, applications, CV profiles, and assessment tests."
    });

    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();


app.UseStaticFiles(); // Enable serving static files (for Swagger UI custom CSS)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobManagement API v1");
        c.InjectStylesheet("/swagger/SwaggerDark.css");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

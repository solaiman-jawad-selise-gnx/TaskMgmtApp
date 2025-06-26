using Application;
using Application.RepositoryInterfaces;
using Asp.Versioning;
using Infrastructure.DB;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Presentation.Middleware;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add AppDbContext to the service container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adding Service dependencies and injecting db context dependencies
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddApplicationDependencies();

builder.Services.AddAutoMapper(typeof(Program));
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
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
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(Role.Admin.ToString()));

    options.AddPolicy("ManagerOrAdmin", policy =>
        policy.RequireRole(Role.Admin.ToString(), Role.Manager.ToString()));

    options.AddPolicy("EmployeesOnly", policy =>
        policy.RequireRole(Role.Employee.ToString()));
});

// Add Distributed Cache for rate limiting
builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(
    options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new HeaderApiVersionReader("api-version"); 
        
    }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
builder.Services.AddSwaggerGen();

// Replace default logging
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Authentication middleware
app.UseAuthentication();
app.UseAuthorization();

// Adding Middleware for rate limiting
app.UseMiddleware<RateLimitingMiddleware>();

// Adding Middleware for exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthorization();
app.MapControllers();

app.Run();

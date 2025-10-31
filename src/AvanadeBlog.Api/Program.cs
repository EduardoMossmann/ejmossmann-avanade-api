using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using AvanadeBlog.Application.Middleware;
using AvanadeBlog.Domain.Roles;
using AvanadeBlog.Infrastructure.Data;
using AvanadeBlog.Infrastructure.Data.Extensions;
using AvanadeBlog.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, serviceProvider) => serviceProvider
   .ReadFrom.Configuration(hostBuilderContext.Configuration)
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Console());

builder.Services.AddDbContext<AvanadeBlogDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
    {
        opts.EnableSensitiveDataLogging();
    }
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });


builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(IdentityUserAccessRoles.USER, policy => policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, IdentityUserAccessRoles.USER));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddValidators();
builder.Services.AddApplicationServices();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AvanadeBlog.Api" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization token using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });

    var basePath = AppContext.BaseDirectory;
    foreach (var file in Directory.EnumerateFiles(basePath, "*.xml", SearchOption.AllDirectories))
    {
        c.IncludeXmlComments(file);
    }
    c.DocInclusionPredicate((name, api) => true);
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvanadeBlog.Api"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

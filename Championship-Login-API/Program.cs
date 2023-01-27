using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DatabaseProject;
using CoreAPI.Utils;
using CoreAPI.Services;
using CoreAPI.Business;
using CoreAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenConfigurations").GetSection("JwtKey").Value);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.OperationFilter<AuthResponsesOperationFilter>();

    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Championship - API",
        Version = "v1",
        Description = "ASP.NET Core Web API para Acelera Senior",
        TermsOfService = new Uri("https://example.com/terms")
    });

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"Exemplo:'Bearer 12345abcdef' "
    });
});

builder.Services.AddDbContext<ChampionContext>();


builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
var connectionString = configuration.GetConnectionString("ChampionConnection");

void BuildOptions(DbContextOptionsBuilder options) => options
    .UseSqlServer(connectionString)
    .EnableSensitiveDataLogging();

#region Dependency Injection
builder.Services.AddChampionshipDependencies();
#endregion

builder.Services.AddDbContext<ChampionContext>(BuildOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Text;
using ChampionshipAPI;
using ChampionshipAPI.Business;
using ChampionshipAPI.Repository;
using DatabaseProject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenConfigurations").GetSection("JwtKey").Value);

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
builder.Services.AddTransient<ChampionshipBusiness>();
builder.Services.AddTransient<ChampionshipRepository>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


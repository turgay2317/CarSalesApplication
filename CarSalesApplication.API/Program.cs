using System.Text;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.BLL.Services;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.Core.Helper;
using CarSalesApplication.DAL;
using CarSalesApplication.DAL.Interfaces;
using CarSalesApplication.DAL.Repositories;
using CarSalesApplication.Presentation.Middleware;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Elasticsearch;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

IConfiguration configuration = builder.Configuration;
var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Microsoft logları Warning ve üstü
    .MinimumLevel.Override("System", LogEventLevel.Warning) // System logları Warning ve üstü
    .WriteTo.Console(new CompactJsonFormatter(), restrictedToMinimumLevel:LogEventLevel.Debug) // Konsolda Minimum Debug-level
    .WriteTo.File(new ElasticsearchJsonFormatter(), "logs/log-.json", rollingInterval:RollingInterval.Hour, restrictedToMinimumLevel:LogEventLevel.Information) // Dosyada Minimum Information-level 
    .CreateLogger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Host.UseSerilog();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:9500/realms/CarSales"; // Keycloak Realm URL
        options.Audience = "CarSales"; // Keycloak Client ID
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:9500/realms/CarSales",
            ValidateAudience = true,
            ValidAudience = "account",
            ValidateLifetime = true,
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var client = new HttpClient();
                var response = client.GetAsync("http://localhost:9500/realms/CarSales/protocol/openid-connect/certs").Result;
                var keys = new JsonWebKeySet(response.Content.ReadAsStringAsync().Result);
                return keys.Keys;
            }
        };
    });


builder.Services.AddAuthorization(options =>
{
    // Admin Role
    options.AddPolicy("Admin", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                c.Type == "realm_access" &&
                c.Value.Contains(UserStatus.Admin.ToString()))));
    // User Role
    options.AddPolicy("User", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                c.Type == "realm_access" &&
                c.Value.Contains(UserStatus.User.ToString()))));
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Bütün projeleri tarar
builder.Services.AddSingleton(sp =>
{
    var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("cars");

    return new ElasticsearchClient(settings);
});
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddHttpClient<KeycloakHelper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseMiddleware<LogMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
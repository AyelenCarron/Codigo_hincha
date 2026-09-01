using System.Text;
using CodigoHincha.API.Middleware;
using CodigoHincha.DAL.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

var jwtKey = configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT key was not found.");

var jwtIssuer = configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("JWT issuer was not found.");

var jwtAudience = configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("JWT audience was not found.");

var jwtExpirationMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes");

builder.Services.AddControllers();

builder.Services.AddDbContext<CodigoHinchaDbContext>(options =>
{
    options
        .UseLazyLoadingProxies()
        .UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<CodigoHincha.DAL.interfaces.IUnitOfWork,
    CodigoHincha.DAL.EntityFramework.EFUnitOfWork>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

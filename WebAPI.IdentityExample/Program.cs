using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AuthDB
builder.Services.AddDbContext<AuthDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB"))
    );

// ASP.Net Core Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// JWTService
builder.Services.AddOptions<JWTServiceOptions>()
    .Bind(builder.Configuration.GetRequiredSection(JWTServiceOptions.JWTService))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IJWTService, JWTService>();

// AuthService
builder.Services.AddScoped<IAuthService, AuthService>();


// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// JWT Bearer
.AddJwtBearer(options =>
{
    var jwtOpts = builder.Configuration
        .GetRequiredSection(JWTServiceOptions.JWTService)
        .Get<JWTServiceOptions>();

    var secretBytes = Encoding.UTF8.GetBytes(jwtOpts!.Secret);

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtOpts!.ValidIssuer,
        ValidAudience = jwtOpts!.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Clean database after each startup
    var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();

    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    await authService.SeedRoles();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

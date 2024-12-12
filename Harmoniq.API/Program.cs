using System.Text;
using Harmoniq.API.Middlewares;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.BLL.Interfaces.Tokens;
using Harmoniq.BLL.Interfaces.UserManagement;
using Harmoniq.BLL.Mapping;
using Harmoniq.BLL.Services.Albums;
using Harmoniq.BLL.Services.ContentCreatorAccount;
using Harmoniq.BLL.Services.Tokens;
using Harmoniq.BLL.Services.UserManagement;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.DAL.Repositories.Albums;
using Harmoniq.DAL.Repositories.ContentCreatorAccount;
using Harmoniq.DAL.Repositories.UserManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("3.0.1", new OpenApiInfo { Title = "My API", Version = "3.0.1" });

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter JWT token in format: Bearer {your_token}",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                new string[] { }
            }
        }
    );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IBearerTokenManagement, BearerTokenManagement>();
builder.Services.AddScoped<IContentCreatorProfileRepository, ContentCreatorProfileRepository>();
builder.Services.AddScoped<IContentCreatorProfileService, ContentCreatorProfileService>();
builder.Services.AddScoped<IAlbumCreatorRepository, AlbumCreatorRepository>();
builder.Services.AddScoped<IAlbumCreatorService, AlbumCreatorService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder
    .Services.AddAuthentication(options =>
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/3.0.1/swagger.json", "My API 3.0.1");
    });
}

app.UseMiddleware<JwtAuthorizationMiddleware>();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



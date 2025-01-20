using System.Text;
using FluentValidation;
using Harmoniq.API.Middlewares;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.BLL.Interfaces.Albums.NewAlbum;
using Harmoniq.BLL.Interfaces.AlbumSongs;
using Harmoniq.BLL.Interfaces.AWS;
using Harmoniq.BLL.Interfaces.Cart;
using Harmoniq.BLL.Interfaces.CartAlbums;
using Harmoniq.BLL.Interfaces.CartPurchase;
using Harmoniq.BLL.Interfaces.ContentConsumerAccount;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.BLL.Interfaces.Discography;
using Harmoniq.BLL.Interfaces.Favorites;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.BLL.Interfaces.RoleChecker;
using Harmoniq.BLL.Interfaces.Stats;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.BLL.Interfaces.Tokens;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Harmoniq.BLL.Interfaces.Wishlist;
using Harmoniq.BLL.Mapping;
using Harmoniq.BLL.Services.AlbumManagement;
using Harmoniq.BLL.Services.Albums;
using Harmoniq.BLL.Services.AlbumSongs;
using Harmoniq.BLL.Services.AWS;
using Harmoniq.BLL.Services.Cart;
using Harmoniq.BLL.Services.CartAlbums;
using Harmoniq.BLL.Services.CartPurchase;
using Harmoniq.BLL.Services.ContentConsumerAccount;
using Harmoniq.BLL.Services.ContentCreatorAccount;
using Harmoniq.BLL.Services.Discography;
using Harmoniq.BLL.Services.Favorites;
using Harmoniq.BLL.Services.PurchasedAlbums;
using Harmoniq.BLL.Services.RoleChecker;
using Harmoniq.BLL.Services.Stats;
using Harmoniq.BLL.Services.Stripe;
using Harmoniq.BLL.Services.Tokens;
using Harmoniq.BLL.Services.UserContext;
using Harmoniq.BLL.Services.UserManagement;
using Harmoniq.BLL.Services.Wishlist;
using Harmoniq.BLL.Validators;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.AlbumSongs;
using Harmoniq.DAL.Interfaces.Cart;
using Harmoniq.DAL.Interfaces.CartAlbums;
using Harmoniq.DAL.Interfaces.CartPurchase;
using Harmoniq.DAL.Interfaces.ContentConsumerAccount;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.Favorites;
using Harmoniq.DAL.Interfaces.PurchasedAlbums;
using Harmoniq.DAL.Interfaces.Stats;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.DAL.Interfaces.Wishlist;
using Harmoniq.DAL.Repositories.AlbumManagement;
using Harmoniq.DAL.Repositories.Albums;
using Harmoniq.DAL.Repositories.AlbumSongs;
using Harmoniq.DAL.Repositories.Cart;
using Harmoniq.DAL.Repositories.CartAlbums;
using Harmoniq.DAL.Repositories.CartPurchase;
using Harmoniq.DAL.Repositories.ContentConsumerAccount;
using Harmoniq.DAL.Repositories.ContentCreatorAccount;
using Harmoniq.DAL.Repositories.Favorites;
using Harmoniq.DAL.Repositories.PurchasedAlbums;
using Harmoniq.DAL.Repositories.Stats;
using Harmoniq.DAL.Repositories.UserManagement;
using Harmoniq.DAL.Repositories.Wishlist;
using Harmoniq.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;

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

builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IBearerTokenManagement, BearerTokenManagement>();
builder.Services.AddScoped<IContentCreatorProfileRepository, ContentCreatorProfileRepository>();
builder.Services.AddScoped<IContentCreatorProfileService, ContentCreatorProfileService>();
builder.Services.AddScoped<INewAlbumRepository, NewAlbumRepository>();
builder.Services.AddScoped<INewAlbumService, NewAlbumService>();
builder.Services.AddScoped<IAlbumSongsRepository, AlbumSongsRepository>();
builder.Services.AddScoped<IAlbumSongsService, AlbumSongsService>();
builder.Services.AddScoped<IContentConsumerAccountRepository, ContentConsumerAccountRepository>();
builder.Services.AddScoped<IContentConsumerAccountService, ContentConsumerAccountService>();
builder.Services.AddScoped<ICreateStripeProductService, CreateStripeProductService>();
builder.Services.AddScoped<IUserRoleCheckerService, UserRoleCheckerService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAlbumCheckoutRepository, AlbumCheckoutRepository>();
builder.Services.AddScoped<IAlbumCheckoutService, AlbumCheckoutService>();
builder.Services.AddScoped<ICheckoutSessionService, CheckoutSessionService>();
builder.Services.AddScoped<IAlbumManagementService, AlbumManagementService>();
builder.Services.AddScoped<IAlbumManagementRepository, AlbumManagementRepository>();
builder.Services.AddScoped<ICloudImageService, CloudImageService>();
builder.Services.AddScoped<IDiscographyService, DiscographyService>();
builder.Services.AddScoped<ICloudTrackService, CloudTrackService>();
builder.Services.AddScoped<IFavoritesAlbumsService, FavoritesAlbumsService>();
builder.Services.AddScoped<IFavoritesAlbumsRepository, FavoritesAlbumsRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<ICartAlbumsService, CartAlbumsService>();
builder.Services.AddScoped<ICartAlbumsRepository, CartAlbumsRepository>();
builder.Services.AddScoped<ICartCheckoutSessionService, CartCheckoutSessionService>();
builder.Services.AddScoped<ICartPurchaseService, CartPurchaseService>();
builder.Services.AddScoped<ICartPurchaseRepository, CartPurchaseRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();



builder.Services.Configure<StripeModel>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<PriceService>();


builder.Services.AddScoped<IValidator<AlbumSongsDto>, AlbumSongsValidator>();
builder.Services.AddScoped<IValidator<AlbumDto>, AlbumValidator>();
builder.Services.AddScoped<IValidator<ContentCreatorDto>, ContentCreatorValidator>();
builder.Services.AddScoped<IValidator<ContentConsumerDto>, ContentConsumerValidator>();
builder.Services.AddScoped<IValidator<EditContentCreatorProfileDto>, EditContentCreatorProfileValidator>();




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



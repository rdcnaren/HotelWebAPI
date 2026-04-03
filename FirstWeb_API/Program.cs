using FirstWeb_API.Data;
using FirstWeb_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using RoyalHotel.DTO;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtToken")["Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Key),
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();

        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter 'Bearer' followed by a space and the JWT token."
            }
        };

        document.Security = 
        [
            new OpenApiSecurityRequirement
            {
                { new OpenApiSecuritySchemeReference("Bearer"), new List<string>() }
            }
        ];

        return Task.CompletedTask;
    });
});
builder.Services.AddAutoMapper(o =>
{
    o.CreateMap<Hotel, HotelCreateDTO>().ReverseMap();
    o.CreateMap<Hotel, HotelUpdateDTO>().ReverseMap();
    o.CreateMap<Hotel, HotelDTO>().ReverseMap();
    o.CreateMap<HotelUpdateDTO, HotelDTO>().ReverseMap();
    o.CreateMap<User, UserDTO>().ReverseMap();
    o.CreateMap<HotelAmenities, HotelAmenitiesCreateDTO>().ReverseMap();
    o.CreateMap<HotelAmenities, HotelAmenitiesUpdateDTO>().ReverseMap();
    o.CreateMap<HotelAmenities, HotelAmenitiesDTO>().
    ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel != null ? src.Hotel.Name : null));
    o.CreateMap<HotelAmenitiesDTO, HotelAmenities>();
});

builder.Services.AddScoped<FirstWeb_API.Services.IAuthService, FirstWeb_API.Services.AuthService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("*"));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

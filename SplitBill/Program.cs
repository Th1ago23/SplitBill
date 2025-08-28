using Application.Service;
using Application.Service.ContextUser;
using Application.Service.JWT;
using Application.Service.Mapper.ExpenseMapper;
using Application.Service.Mapper.GroupMapper;
using Application.Service.Mapper.UserMapper;
using Application.Utils.Validator;
using Domain.Interface.Context;
using Domain.Interface.Database;
using Domain.Interface.Mapper.ExpenseMapper;
using Domain.Interface.Mapper.GroupMapper;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Domain.Interface.Token;
using Domain.Interface.Utils;
using Infrastructure.Database;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IUserMP, UserMP>();
builder.Services.AddScoped<IObjectValidator, ObjectValidator>();
builder.Services.AddScoped<IGroupMP, GroupMP>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseMP, ExpenseMP>();



builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT aqui",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DbConfig>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("Infrastructure")));



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters.ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 };
    options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .WithOrigins("http://localhost:4200") // Frontend específico
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // Importante para JWT
});


WebApplication app = builder.Build();

// No Program.cs, substituir a configuração atual:

// E na configuração da aplicação:

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

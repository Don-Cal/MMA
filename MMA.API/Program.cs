using MMA.API.Configurations;
using MMA.API.Data;
using MMA.API.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("MMADbConnection");
builder.Services.AddDbContext<MMADbContext>(options => options.UseSqlServer(connString));

builder.Services.AddIdentityCore<ApiUser>()
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<MMADbContext>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllers();//.AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
  {
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
      Description = "JWT Authorization header useing the bearer scheme"
                  + "Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\n"
                  + "Example: \"Bearer 1safsfsdfdfd\"",
      Name = "Authorization",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.ApiKey,
      Scheme = "Bearer",
      BearerFormat = "JWT",
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {new OpenApiSecurityScheme{Reference = new OpenApiReference
      {
        Id = "Bearer",
        Type = ReferenceType.SecurityScheme
      }}, new List<string>() }
    });

});

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll",
      b => b.AllowAnyMethod()
      .AllowAnyHeader()
      .AllowAnyOrigin());
});

builder.Services.AddAuthentication(options => {
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
  };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

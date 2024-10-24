using CustomIdentity.Context;
using CustomIdentity.Manager;
using CustomIdentity.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // => Issuer validation open
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // => appsettings
            ValidateAudience=true, // => Audience validation open
            ValidAudience = builder.Configuration["Jwt:Audience"],  // => appsetting
            ValidateLifetime =true, // => token lifetime defining
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)) // => appsettings secretkey, ! for I m sure its not null
        };
    });
    


// ? Getting connection string from appsettings ?
var connectionString = builder.Configuration.GetConnectionString("default");

// ? Adding service Dbcontext - Sql Server ?
builder.Services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(connectionString)
.EnableSensitiveDataLogging() // => For seeing migration tsql codes in terminal
.EnableDetailedErrors());

builder.Services.AddScoped<IUserService,UserManager>(); // => Dependency injection scoped lifecycle



var app = builder.Build();

// Configure the HTTP request pipeline.
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

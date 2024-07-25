using ExpenseTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Infrastructure.Repositories;
using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("dbcs"), b => b.MigrationsAssembly("ExpenseTracker.Infrastructure")), ServiceLifetime.Scoped);
builder.Services.AddScoped<IExpenseRepository, TransactionRepository>();
builder.Services.AddScoped<IAuthRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped<IExpenseService, TransactionService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ExpenseTrackerDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? ""))
    };
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

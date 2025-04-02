using System.Reflection;
using System.Security.Cryptography;
using FluentValidation;
using FluentValidation.AspNetCore;
using HelpWithAllApp.Middlewares;
using HelpWithAll.Core.Options;
using HelpWithAll.Infrastructure.Repositories;
using HelpWithAll.Core.Repositories.Base;
using HelpWithAll.Infrastructure.Service;
using HelpWithAll.Core.Service.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HelpWithAll.Core.Models;
using HelpWithAll.Core.Validators;
using HelpWithAll.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<HelperDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("HelperDatabase");
    options.UseNpgsql(connectionString);
});
builder.Services.AddDbContext<AuthenticationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("HelperDatabase");
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IHelperRepository, HelperDapperRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerDapperRepository>();
builder.Services.AddScoped<IHttpLogRepository, HttpDapperRepository>();
builder.Services.AddScoped<IHttpLogger, HttpLogger>();
builder.Services.Configure<DatabaseOptions>(options => {
    var connectionString = builder.Configuration.GetConnectionString("HelperDatabase");
    options.ConnectionString = connectionString!;
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {})
    .AddEntityFrameworkStores<AuthenticationDbContext>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<IValidator<Helper>, HelperValidator>();
builder.Services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme,
        configureOptions: options =>
        {
            
        });


builder.Services.AddAuthorization(options => {
    options.AddPolicy(
        name: "HelperPolicy",
        configurePolicy: policyBuilder => {
            policyBuilder
                .RequireRole("Admin")
                .RequireRole("User");
        }
    );
});


var app = builder.Build();

// Ensure routing is enabled

app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseWebSockets();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();

using System.Reflection;
using System.Security.Cryptography;
using FluentValidation;
using HelpWithAllApp.Middlewares;
using HelpWithAllApp.Options;
using HelpWithAllApp.Repositories;
using HelpWithAllApp.Repositories.Base;
using HelpWithAllApp.Service;
using HelpWithAllApp.Service.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IHelperRepository, HelperDapperRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerDapperRepository>();
builder.Services.AddScoped<IHttpLogRepository, HttpDapperRepository>();
builder.Services.AddScoped<IHttpLogger, HttpLogger>();
builder.Services.Configure<DatabaseOptions>(options => {
    var connectionString = builder.Configuration.GetConnectionString("HelperDatabase");
    options.ConnectionString = connectionString!;
});
builder.Services.AddValidatorsFromAssemblies(new Assembly[] {
    Assembly.GetExecutingAssembly(),
});


var app = builder.Build();

// Ensure routing is enabled

app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
    _ = endpoints.MapDefaultControllerRoute();
});
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();

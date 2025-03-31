using HelpWithAllApp.Options;
using HelpWithAllApp.Repositories;
using HelpWithAllApp.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IHelperRepository, HelperDapperRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerDapperRepository>();
builder.Services.Configure<DatabaseOptions>(options => {
    var connectionString = builder.Configuration.GetConnectionString("HelperDatabase");
    options.ConnectionString = connectionString!;
});

var app = builder.Build();

// Ensure routing is enabled

//app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization(); // If you have authentication/authorization

// Map controllers and default routes
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Maps API controllers
    endpoints.MapDefaultControllerRoute(); // Ensures MVC default routing works
});

app.Run();

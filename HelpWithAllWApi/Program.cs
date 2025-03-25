using HelpWithAllWApi.Repositories.Base;
using HelpWithAllWApi.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IHelperRepository, HelperJsonRepository>();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.Run();

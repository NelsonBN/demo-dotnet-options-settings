using Demo.Api.Options;
using Demo.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DemoOptions>(builder.Configuration);
builder.Services.ConfigureOptions<ExampleOptionsSetup>();

builder.Services.AddSingleton<IDemo11Service, Demo11Service>();
builder.Services.AddSingleton<IDemo12Service, Demo12Service>();

builder.Services.AddScoped<IDemo21Service, Demo21Service>();
builder.Services.AddScoped<IDemo22Service, Demo22Service>();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

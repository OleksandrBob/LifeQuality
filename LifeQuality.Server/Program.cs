using LifeQuality.Core.Services;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddDbContext<DataContext>(o =>
    o.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=LifeQuality;Integrated Security=True"));

builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddTransient<IAnalysisService, AnalysisService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

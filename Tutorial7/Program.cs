using Microsoft.EntityFrameworkCore;
using Tutorial7.Data;
using Tutorial7.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IPcService, PcService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

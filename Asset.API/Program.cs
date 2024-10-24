using Asset.Domain;
using FinancialTask.Infrastructure;
using FinancialTask.Infrastructure.Repositories;
using FinancialTask.Services.Fintacharts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AssetContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IFintachartsService), typeof(FintachartsService));
builder.Services.AddScoped(typeof(IAssetRepository), typeof(AssetRepository));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "FintachartConfigUri",
                      builder =>
                      {
                          builder.WithOrigins("https://platform.fintacharts.com");
                      });
});

Console.WriteLine("asdasd");

var app = builder.Build();
app.UseWebSockets();
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

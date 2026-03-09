using FilmanduOS.Data;
using FilmanduOS.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conectionDbContext = builder.Configuration.GetConnectionString("FilmeConnection");

builder.Services.AddDbContext<BancoDados>(options => options.UseMySql(conectionDbContext, ServerVersion.AutoDetect(conectionDbContext)));
builder.Services.AddScoped<MusicasService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

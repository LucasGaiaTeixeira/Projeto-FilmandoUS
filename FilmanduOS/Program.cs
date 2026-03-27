using FilmanduOS.Data;
using FilmanduOS.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conectionDbContext = builder.Configuration.GetConnectionString("FilmeConnection");

builder.Services.AddDbContext<BancoDados>(options => options.UseLazyLoadingProxies().UseMySql(conectionDbContext, ServerVersion.AutoDetect(conectionDbContext)));
//esse use lazyLoadingProxies serve para: 
builder.Services.AddScoped<MusicasService>();


builder.Services.AddControllers().AddNewtonsoftJson();//pacote newtonsoft
//pacote newtonsoft serve para: 

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//estou aplicando as dependencias buildando um novo serviço que é o AutoMapper aonde ele esta no Dominio do App completo e procurando no projeto inteiro mapeamento para fazer de dto para models
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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








using ClearVolt.Data.Data;
using ClearVolt.DataIA;
using ClearVolt.Interfaces;
using ClearVolt.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Configurando a conexão com banco de dados
builder.Services.AddDbContext<ClearVoltDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
});

//Add services to the container.
builder.Services.AddSingleton<ClearVoltIAService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClearVolt API", Version = "v1" });
});

builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<IRoleInterface, RoleService>();
builder.Services.AddScoped<IPessoaInterface, PessoaService>();
builder.Services.AddScoped<IConfiguracaoColetaInterface, ConfiguracaoColetaService>();
builder.Services.AddScoped<IDispositivoInterface, DispositivoService>();
builder.Services.AddScoped<IDadoColetadoInterface, DadoColetadoService>();
builder.Services.AddScoped<IDispositivoInterface, DispositivoService>();

var app = builder.Build();

var iaService = app.Services.GetRequiredService<ClearVoltIAService>();
iaService.TrainModel();

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

public partial class Program { }
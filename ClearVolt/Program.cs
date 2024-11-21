using ClearVolt.Data.Data;
using ClearVolt.Interfaces;
using ClearVolt.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

//Configurando a conexão com banco de dados
builder.Services.AddDbContext<ClearVoltDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
        //sqlOptions => sqlOptions.MigrationsAssembly("ClearVolt.Data"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<IRoleInterface, RoleService>();
builder.Services.AddScoped<IPessoaInterface, PessoaService>();
builder.Services.AddScoped<IConfiguracaoColetaInterface, ConfiguracaoColetaService>();
builder.Services.AddScoped<IDispositivoInterface, DispositivoService>();
builder.Services.AddScoped<IDadoColetadoInterface, DadoColetadoService>();

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

builder.Services.AddScoped<IDispositivoInterface, DispositivoService>();
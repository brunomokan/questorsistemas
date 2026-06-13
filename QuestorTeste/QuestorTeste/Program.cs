using Microsoft.EntityFrameworkCore;
using QuestorTeste.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<QuestorTeste.Entities.Banco.Ports.IBancoInputPort, QuestorTeste.Entities.Banco.Domain.Services.BancoService>();
builder.Services.AddScoped<QuestorTeste.Entities.Banco.Ports.IBancoOutputPort, QuestorTeste.Entities.Banco.Adapters.Outbound.BancoPersistenceAdapter>();

builder.Services.AddScoped<QuestorTeste.Entities.Boleto.Ports.IBoletoInputPort, QuestorTeste.Entities.Boleto.Domain.Services.BoletoService>();
builder.Services.AddScoped<QuestorTeste.Entities.Boleto.Ports.IBoletoOutputPort, QuestorTeste.Entities.Boleto.Adapters.Outbound.BoletoPersistenceAdapter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
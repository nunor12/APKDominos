// Program.cs — ponto de entrada e configuração da aplicação
// Em .NET 6+ usa-se o "minimal hosting model" (sem Startup.cs separado)
//
// Equivalente ao ficheiro index.js/app.js no Express.js:
//   const express = require('express');
//   const app = express();
//   app.use(express.json());
//   app.listen(3000);

var builder = WebApplication.CreateBuilder(args);

// ── Registo de Serviços (equivalente a configurar providers/middleware no Express) ──

// Adiciona suporte para Controllers (MVC)
builder.Services.AddControllers();

// Adiciona geração automática de documentação OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "HelloWorldAPI — Produtos",
        Version = "v1",
        Description = "API de demonstração para aprender .NET Web API"
    });
});

// ── Construção da Aplicação ──
var app = builder.Build();

// ── Pipeline de Middleware ──
// A ordem importa — é processada de cima para baixo em cada request

if (app.Environment.IsDevelopment())
{
    // Swagger apenas em desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelloWorldAPI v1");
        c.RoutePrefix = "swagger"; // Swagger em /swagger
    });
}

app.UseHttpsRedirection();

// Mapeia os controllers com os seus atributos de rota
app.MapControllers();

// Rota de boas-vindas na raiz
app.MapGet("/", () => new
{
    mensagem = "Bem-vindo à HelloWorldAPI!",
    swagger = "/swagger",
    endpoints = new[]
    {
        "GET  /api/produtos",
        "GET  /api/produtos/{id}",
        "GET  /api/produtos/resumo",
        "POST /api/produtos",
        "PUT  /api/produtos/{id}",
        "DELETE /api/produtos/{id}"
    }
});

app.Run();

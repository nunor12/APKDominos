using HelloWorldAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldAPI.Controllers;

/// <summary>
/// Controller de Produtos — equivalente a um router/controller no Express.js.
///
/// [ApiController]       → ativa validação automática do modelo e outros comportamentos de API
/// [Route("api/[controller]")] → define a rota base como /api/produtos
///                               [controller] é substituído automaticamente pelo nome da classe
///                               sem o sufixo "Controller"
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    // Simula uma "base de dados" em memória usando uma lista estática.
    // Em produção, injectaríamos um repositório ou DbContext via Dependency Injection.
    private static readonly List<Produto> _produtos = new()
    {
        new Produto { Id = 1, Nome = "Laptop",   Preco = 999.99m, Categoria = "Eletrónica", Ativo = true },
        new Produto { Id = 2, Nome = "Rato",     Preco = 29.99m,  Categoria = "Eletrónica", Ativo = true },
        new Produto { Id = 3, Nome = "Mesa",     Preco = 199.99m, Categoria = "Mobiliário", Ativo = false },
        new Produto { Id = 4, Nome = "Cadeira",  Preco = 149.99m, Categoria = "Mobiliário", Ativo = true },
    };
    private static int _nextId = 5;

    // -------------------------------------------------------------------------
    // GET /api/produtos
    // Suporta filtragem opcional por categoria e por estado activo.
    //
    // JS equivalente:
    //   app.get('/api/produtos', (req, res) => res.json(produtos))
    // -------------------------------------------------------------------------
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAll(
        [FromQuery] string? categoria = null,
        [FromQuery] bool? ativo = null)
    {
        // LINQ: Where é o equivalente de Array.filter() em JS
        var resultado = _produtos.AsEnumerable();

        if (categoria is not null)
            resultado = resultado.Where(p => p.Categoria == categoria);

        if (ativo is not null)
            resultado = resultado.Where(p => p.Ativo == ativo);

        return Ok(resultado);
    }

    // -------------------------------------------------------------------------
    // GET /api/produtos/5
    // -------------------------------------------------------------------------
    [HttpGet("{id:int}")]
    public ActionResult<Produto> GetById(int id)
    {
        // FirstOrDefault é o equivalente de Array.find() em JS — devolve null se não encontrar
        var produto = _produtos.FirstOrDefault(p => p.Id == id);

        if (produto is null)
            return NotFound(new { mensagem = $"Produto com id {id} não encontrado." });

        return Ok(produto);
    }

    // -------------------------------------------------------------------------
    // GET /api/produtos/resumo
    // Demonstra LINQ avançado: agrupamento e projecção (Select)
    // -------------------------------------------------------------------------
    [HttpGet("resumo")]
    public ActionResult GetResumo()
    {
        // GroupBy é semelhante ao lodash _.groupBy() ou reduce() manual em JS
        var resumo = _produtos
            .GroupBy(p => p.Categoria)
            .Select(grupo => new
            {
                Categoria = grupo.Key,
                Total = grupo.Count(),
                PrecoMedio = grupo.Average(p => p.Preco),
                PrecoTotal = grupo.Sum(p => p.Preco)
            });

        return Ok(resumo);
    }

    // -------------------------------------------------------------------------
    // POST /api/produtos
    // [FromBody] diz ao .NET para ler o corpo JSON do request e deserializar para Produto
    // -------------------------------------------------------------------------
    [HttpPost]
    public ActionResult<Produto> Create([FromBody] Produto produto)
    {
        produto.Id = _nextId++;
        _produtos.Add(produto);

        // CreatedAtAction devolve 201 Created com o header Location a apontar para o novo recurso
        // Equivalente a res.status(201).json(produto) no Express
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    // -------------------------------------------------------------------------
    // PUT /api/produtos/5
    // -------------------------------------------------------------------------
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Produto produto)
    {
        if (id != produto.Id)
            return BadRequest(new { mensagem = "O id da rota não corresponde ao id do body." });

        var indice = _produtos.FindIndex(p => p.Id == id);
        if (indice == -1)
            return NotFound(new { mensagem = $"Produto com id {id} não encontrado." });

        _produtos[indice] = produto;
        return NoContent(); // 204 — actualizado com sucesso, sem body de resposta
    }

    // -------------------------------------------------------------------------
    // DELETE /api/produtos/5
    // -------------------------------------------------------------------------
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var produto = _produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null)
            return NotFound(new { mensagem = $"Produto com id {id} não encontrado." });

        _produtos.Remove(produto);
        return NoContent(); // 204
    }
}

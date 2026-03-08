# Roteiro de Aprendizagem: C# e .NET para Developers Python/JavaScript

> **Público-alvo:** Developer Júnior com experiência em Python e JavaScript (ES6+) a preparar uma transição rápida para C# e .NET Full Stack.

---

## 1. Mapeamento de Conceitos

### Python → C#

| Python | C# | Notas |
|---|---|---|
| `dict` | `Dictionary<TKey, TValue>` | Tipagem genérica obrigatória |
| `list` | `List<T>` | Equivalente direto |
| `tuple` | `(int, string)` (Value Tuple) | `var p = (42, "Alice");` |
| `None` | `null` | Tipos referência são anuláveis por padrão |
| `def func():` | `void Func()` | Tipo de retorno obrigatório |
| `class Foo:` | `class Foo { }` | Sintaxe com chavetas |
| Decorators (`@`) | Atributos (`[Route("api")]`) | Ex: `[HttpGet]` nos controllers |
| `__init__` | Construtor com mesmo nome da classe | `public Produto(string nome) { }` |
| f-strings | Interpolação `$"Olá {nome}"` | Idêntico em funcionalidade |
| `try/except` | `try/catch` | `catch (Exception ex)` |
| List comprehensions | LINQ (ver secção 4) | `list.Where(...).Select(...)` |
| `*args` / `**kwargs` | `params T[]` / métodos sobrecarregados | C# favorece sobrecarga de métodos |
| Módulos / `import` | `using` + Namespaces | `using System.Collections.Generic;` |

### JavaScript (ES6+) → C#

| JavaScript | C# | Notas |
|---|---|---|
| `interface IFoo { }` (TS) | `interface IFoo { }` | Sintaxe quase idêntica |
| `class Foo extends Bar` | `class Foo : Bar` | Herança com `:` |
| `class Foo implements IBar` (TS) | `class Foo : IBar` | Mesma sintaxe de herança |
| `const` / `let` | `const` / variável local tipada | `const` em C# é em tempo de compilação |
| `async/await` | `async/await` | Mesma semântica, ver secção 4 |
| `Promise<T>` | `Task<T>` | Equivalente direto |
| `Array.filter()` | `.Where()` (LINQ) | `lista.Where(x => x.Ativo)` |
| `Array.map()` | `.Select()` (LINQ) | `lista.Select(x => x.Nome)` |
| `Array.reduce()` | `.Aggregate()` (LINQ) | `lista.Aggregate(0, (acc, x) => acc + x)` |
| `Array.find()` | `.FirstOrDefault()` (LINQ) | Retorna `null` se não encontrar |
| Template literals `` `Olá ${nome}` `` | `$"Olá {nome}"` | Interpolação de strings |
| Spread `{...obj}` | Sem equivalente direto | Usar `with` em Records (C# 9+) |
| Arrow functions `x => x + 1` | Lambda `x => x + 1` | Sintaxe idêntica |
| Destructuring `const {a, b} = obj` | Deconstruction `var (a, b) = tuple` | Suportado em tuples |
| `typeof` / `instanceof` | `typeof` / `is` / `as` | `if (obj is string s) { }` |
| Módulos ES6 (`import/export`) | Namespaces + `using` | Sem ficheiro de módulo explícito |

---

## 2. Sintaxe Essencial de C#

### 2.1 Tipagem Estática

```csharp
// C# — tipo declarado explicitamente
string nome = "Alice";
int idade = 30;
bool ativo = true;

// Inferência de tipo com 'var' (o compilador determina o tipo)
var produto = "Laptop"; // ainda é string, não é dinâmico

// Tipos anuláveis (nullable) — use ? para permitir null
int? quantidade = null;
string? descricao = null;
```

**Comparação Python/JS:**
```python
# Python — dinâmico, sem declaração de tipo (type hints são opcionais)
nome = "Alice"
```
```js
// JS — dinâmico
let nome = "Alice";
```

### 2.2 Visibilidade (Modificadores de Acesso)

```csharp
public class ContaBancaria
{
    // public  — acessível em todo o lado
    public string Titular { get; set; }

    // private — apenas dentro desta classe
    private decimal _saldo;

    // protected — esta classe e subclasses
    protected string Moeda = "EUR";

    // internal — apenas dentro do mesmo assembly (.dll / projeto)
    internal string CodigoInterno = "XYZ";

    public void Depositar(decimal valor)
    {
        _saldo += valor; // acesso a campo privado dentro da própria classe
    }

    public decimal ObterSaldo()
    {
        return _saldo;
    }
}
```

> **Regra prática:** Campos são sempre `private`; propriedades e métodos são `public` ou `private` consoante o encapsulamento desejado.

### 2.3 Namespaces

```csharp
// Declara o namespace deste ficheiro
namespace MeuApp.Servicos
{
    public class EmailService
    {
        public void Enviar(string destino, string mensagem) { }
    }
}

// Noutro ficheiro — importar o namespace com 'using'
using MeuApp.Servicos;

var servico = new EmailService();
servico.Enviar("user@example.com", "Olá!");
```

C# 10+ suporta **file-scoped namespaces** (sem chavetas):
```csharp
namespace MeuApp.Servicos; // sem { }

public class EmailService { }
```

### 2.4 Properties vs Campos

```csharp
public class Produto
{
    // Propriedade automática (auto-property)
    public string Nome { get; set; }

    // Propriedade com lógica de validação
    private decimal _preco;
    public decimal Preco
    {
        get => _preco;
        set
        {
            if (value < 0) throw new ArgumentException("Preço não pode ser negativo.");
            _preco = value;
        }
    }

    // Init-only — pode ser definida na construção mas não alterada depois
    public string Categoria { get; init; }
}

// Uso
var p = new Produto { Nome = "Laptop", Categoria = "Eletrónica" };
p.Nome = "Laptop Pro"; // OK
// p.Categoria = "Outro"; // ERRO — init-only
```

---

## 3. C# Moderno

### 3.1 LINQ — o "filter/map" do C#

LINQ (Language Integrated Query) permite fazer consultas diretamente sobre coleções, de forma declarativa, tal como `filter`, `map` e `reduce` no JavaScript.

```csharp
var produtos = new List<Produto>
{
    new Produto { Nome = "Laptop", Preco = 999.99m, Categoria = "Eletrónica", Ativo = true },
    new Produto { Nome = "Rato",   Preco = 29.99m,  Categoria = "Eletrónica", Ativo = true },
    new Produto { Nome = "Mesa",   Preco = 199.99m, Categoria = "Mobiliário", Ativo = false },
    new Produto { Nome = "Cadeira",Preco = 149.99m, Categoria = "Mobiliário", Ativo = true },
};

// JS: produtos.filter(p => p.ativo)
var ativos = produtos.Where(p => p.Ativo);

// JS: produtos.map(p => p.nome)
var nomes = produtos.Select(p => p.Nome);

// JS: produtos.filter(p => p.categoria === "Eletrónica").map(p => p.nome)
var nomesEletronica = produtos
    .Where(p => p.Categoria == "Eletrónica")
    .Select(p => p.Nome);

// JS: produtos.reduce((acc, p) => acc + p.preco, 0)
var totalPreco = produtos.Sum(p => p.Preco);

// JS: produtos.find(p => p.nome === "Rato")
var rato = produtos.FirstOrDefault(p => p.Nome == "Rato");

// Ordenar
var ordenados = produtos.OrderBy(p => p.Preco);
var ordenadosDesc = produtos.OrderByDescending(p => p.Preco);

// Agrupar (sem equivalente direto simples em JS vanilla)
var porCategoria = produtos.GroupBy(p => p.Categoria);
foreach (var grupo in porCategoria)
{
    Console.WriteLine($"Categoria: {grupo.Key}");
    foreach (var prod in grupo)
        Console.WriteLine($"  - {prod.Nome}");
}
```

LINQ também suporta **sintaxe de query** (semelhante a SQL):
```csharp
var resultado = from p in produtos
                where p.Ativo && p.Preco > 50
                orderby p.Nome
                select new { p.Nome, p.Preco };
```

### 3.2 Async / Await

A semântica é idêntica ao JavaScript: `async` marca um método como assíncrono e `await` aguarda a conclusão de uma `Task<T>` (equivalente a `Promise<T>`).

```csharp
using System.Net.Http;
using System.Text.Json;

// async Task = async function que não retorna valor
// async Task<T> = async function que retorna T (equivalente a Promise<T>)

public class ProdutoService
{
    private readonly HttpClient _httpClient;

    public ProdutoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Equivalente JS: async function buscarProdutos() { ... }
    public async Task<List<Produto>> BuscarProdutosAsync()
    {
        var resposta = await _httpClient.GetStringAsync("https://api.exemplo.com/produtos");
        var produtos = JsonSerializer.Deserialize<List<Produto>>(resposta);
        return produtos ?? new List<Produto>();
    }

    // Tratamento de erros — igual ao JS
    public async Task<Produto?> BuscarPorIdAsync(int id)
    {
        try
        {
            var json = await _httpClient.GetStringAsync($"https://api.exemplo.com/produtos/{id}");
            return JsonSerializer.Deserialize<Produto>(json);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro na requisição: {ex.Message}");
            return null;
        }
    }
}
```

> **Convenção .NET:** Métodos assíncronos terminam com o sufixo `Async` (ex: `GetAsync`, `SaveAsync`).

---

## 4. Web API — Controller Básico em .NET

A estrutura de um controller em .NET Web API segue o padrão MVC, com rotas declaradas por atributos (equivalente aos decorators do Python/Express.js).

```csharp
using Microsoft.AspNetCore.Mvc;

namespace MeuApp.Controllers
{
    [ApiController]                    // Ativa comportamentos automáticos de API
    [Route("api/[controller]")]        // Rota base: /api/produtos
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        // Injecção de Dependência via construtor (equivalente a providers do Angular)
        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        // GET /api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            var produtos = await _service.GetAllAsync();
            return Ok(produtos); // 200 OK com body JSON
        }

        // GET /api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            var produto = await _service.GetByIdAsync(id);
            if (produto == null)
                return NotFound(); // 404
            return Ok(produto);
        }

        // POST /api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> Create([FromBody] Produto produto)
        {
            var criado = await _service.CreateAsync(produto);
            // 201 Created com header Location apontando para o novo recurso
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT /api/produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
                return BadRequest(); // 400

            await _service.UpdateAsync(produto);
            return NoContent(); // 204
        }

        // DELETE /api/produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent(); // 204
        }
    }
}
```

**Comparação com Express.js:**

| Express.js | .NET Web API |
|---|---|
| `app.get('/produtos', handler)` | `[HttpGet]` num método do Controller |
| `req.params.id` | Parâmetro do método `int id` |
| `req.body` | `[FromBody] Produto produto` |
| `res.json(data)` | `return Ok(data)` |
| `res.status(404).send()` | `return NotFound()` |
| `res.status(201).json(data)` | `return CreatedAtAction(...)` |
| Middleware (`app.use`) | Middleware (`app.UseMiddleware<>`) |
| `module.exports` / `require` | Namespaces + `using` |

---

## 5. Mini-Projeto: Hello World Backend em .NET

> **Objetivo:** Construir uma Web API simples que gere uma lista de produtos em memória, para entender o fluxo de dados no .NET.

O projeto encontra-se na pasta [`HelloWorldAPI/`](./HelloWorldAPI/).

### Estrutura do Projeto

```
HelloWorldAPI/
├── HelloWorldAPI.csproj        # Ficheiro de projeto (.NET)
├── Program.cs                  # Ponto de entrada + configuração da aplicação
├── Controllers/
│   └── ProdutosController.cs   # Endpoints da API
└── Models/
    └── Produto.cs              # Modelo de dados
```

### Como Executar

```bash
# Pré-requisito: .NET SDK 8.0+
# Download: https://dotnet.microsoft.com/download

cd HelloWorldAPI

# Restaurar dependências e executar
dotnet run

# A API ficará disponível em:
# http://localhost:5000
# https://localhost:5001
# Swagger UI: http://localhost:5000/swagger
```

### Endpoints Disponíveis

| Método | URL | Descrição |
|---|---|---|
| `GET` | `/api/produtos` | Lista todos os produtos |
| `GET` | `/api/produtos/{id}` | Obtém produto por ID |
| `POST` | `/api/produtos` | Cria novo produto |
| `PUT` | `/api/produtos/{id}` | Atualiza produto |
| `DELETE` | `/api/produtos/{id}` | Remove produto |

### Fluxo de Dados no .NET

```
Request HTTP
    ↓
Middleware Pipeline (autenticação, logging, CORS, etc.)
    ↓
Router → ProdutosController
    ↓
Método do Controller (ex: GetAll)
    ↓
Service Layer (lógica de negócio)
    ↓
Repositório / Base de dados
    ↓
Resposta serializada para JSON
    ↓
Response HTTP
```

---

## 6. Próximos Passos

1. **Entity Framework Core** — ORM do .NET (equivalente ao SQLAlchemy/Sequelize): mapeia classes C# para tabelas de base de dados.
2. **Dependency Injection** — padrão nativo no .NET, fundamental para testabilidade.
3. **xUnit / NUnit** — frameworks de testes unitários (equivalente ao pytest/Jest).
4. **Swagger / OpenAPI** — documentação automática de APIs (já incluído no projeto Hello World).
5. **Angular** — o frontend da vaga usa Angular; como já conheces JS/TS, a curva de aprendizagem é menor.

---

## Recursos Recomendados

- [Microsoft Learn — C# para iniciantes](https://learn.microsoft.com/pt-pt/dotnet/csharp/tour-of-csharp/)
- [C# para developers Python](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/)
- [ASP.NET Core Web API Tutorial](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)
- [LINQ Tutorial](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)

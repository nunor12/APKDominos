namespace HelloWorldAPI.Models;

/// <summary>
/// Modelo de dados que representa um Produto.
/// Em Python seria apenas um dict ou dataclass; em JS um objeto simples.
/// Em C# usamos uma classe com propriedades tipadas.
/// </summary>
public class Produto
{
    public int Id { get; set; }

    // string? significa que a propriedade é anulável (pode ser null)
    public string? Nome { get; set; }

    // decimal é o tipo correcto para valores monetários (mais precisão que float/double)
    public decimal Preco { get; set; }

    public string? Categoria { get; set; }

    public bool Ativo { get; set; } = true;
}

namespace Entities;

public class ItemCarrinho
{
    public int IdLivro { get; set; }
    public string? TituloLivro { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    public decimal SubTotal => PrecoUnitario * Quantidade;
}
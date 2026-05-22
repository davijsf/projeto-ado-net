namespace Entities;

public class Carrinho
{
    public int IdCliente { get; set; }
    public int? IdVendedor { get; set; }
    public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();

    public decimal Total => Itens.Sum(i => i.SubTotal);
}
namespace Entities;

public class ItemVenda
{
    public int Id { get ; set ;}
    public int Quantidade { get ; set ;}
    public double SubTotal { get ; set ;}

    public int IdLivro { get ; set ;}
    public Livro ?Livro { get ; set ;}

    public int IdVenda { get ; set ;}
    public Venda ?Venda { get ; set ;}
}
namespace Entities;

public class Venda
{
    public int Id { get ; set ;}
    public DateTime Data { get ; set ;}
    public double Total { get ; set ;}

    // chave estrangeira
    public int IdCliente { get ; set ;}
    // prop. de nav
    public Cliente ? Cliente { get ; set ;}

    // chave estrangeira
    public int IdVendedor { get ; set ;}
     // prop. de nav
    public Vendedor ? Vendedor { get ; set ;}
}
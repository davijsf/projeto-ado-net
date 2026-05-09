namespace Data.Repositories.Interfaces;

using Entities;

public interface IVendaRepository
{
    int RegistrarVenda(Venda venda);
    void AdicionarItem(int vendaId, ItemVenda item);
    void RemoverItem(int vendaId, int itemId);
    decimal CalcularTotal(int vendaId);
    List<Venda> ListarVendas();
    List<Venda> ConsultarVendasPorCliente(int clienteId);
}

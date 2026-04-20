using Entities;

namespace Business.Interfaces;

public interface IVendaService
{
    void RegistrarVenda(Venda venda);
    void AdicionarItem(int vendaId, ItemVenda item);
    void RemoverItem(int vendaId, int itemId);
    decimal CalcularTotal(int vendaId);
    List<Venda> ListarVendas();
    List<Venda> ConsultarVendasPorCliente(int clienteId);
}
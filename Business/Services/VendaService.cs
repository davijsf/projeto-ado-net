namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;

public class VendaService : IVendaService
{
    private readonly VendaRepository _repository = new VendaRepository();

    public void RegistrarVenda(Venda venda)
    {
        venda.Id = _repository.RegistrarVenda(venda);
    }

    public void AdicionarItem(int vendaId, ItemVenda item)
    {
        _repository.AdicionarItem(vendaId, item);
    }

    public void RemoverItem(int vendaId, int itemId)
    {
        _repository.RemoverItem(vendaId, itemId);
    }

    public decimal CalcularTotal(int vendaId)
    {
        return _repository.CalcularTotal(vendaId);
    }

    public List<Venda> ListarVendas()
    {
        return _repository.ListarVendas();
    }

    public List<Venda> ConsultarVendasPorCliente(int clienteId)
    {
        return _repository.ConsultarVendasPorCliente(clienteId);
    }
}

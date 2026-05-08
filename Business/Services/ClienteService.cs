namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;

public class ClienteService : IClienteService
{
    private readonly ClienteRepository _repository = new ClienteRepository();

    public void CadastrarCliente(Cliente cliente)
    {
        _repository.CadastrarCliente(cliente);
    }

    public List<Cliente> ListarClientes()
    {
        return _repository.ListarClientes();
    }

    public void AtualizarCliente(Cliente cliente)
    {
        _repository.AtualizarCliente(cliente);
    }

    public void RemoverCliente(int id)
    {
        _repository.RemoverCliente(id);
    }
}

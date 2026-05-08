namespace Data.Repositories;

using Entities;

public interface IClienteRepository
{
    void CadastrarCliente(Cliente cliente);
    List<Cliente> ListarClientes();
    void AtualizarCliente(Cliente cliente);
    void RemoverCliente(int id);
}

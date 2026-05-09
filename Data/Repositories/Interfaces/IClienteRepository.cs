namespace Data.Repositories.Interfaces;

using Entities;

public interface IClienteRepository
{
    void CadastrarCliente(Cliente cliente);
    List<Cliente> ListarClientes();
    void AtualizarCliente(Cliente cliente);
    void RemoverCliente(int id);
    Cliente ? BuscarPorCpf(string cpf);
    Cliente ? BuscarPorId(int id);
}

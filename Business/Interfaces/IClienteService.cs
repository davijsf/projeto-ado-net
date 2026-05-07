namespace Business.Interfaces;

using Entities;

public interface IClienteService
{
    void CadastrarCliente(Cliente cliente);
    List<Cliente> ListarClientes();
    void AtualizarCliente(Cliente cliente);
    void RemoverCliente(int id);
}
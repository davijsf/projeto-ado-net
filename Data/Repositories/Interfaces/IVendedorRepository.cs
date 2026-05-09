namespace Data.Repositories.Interfaces;

using Entities;

public interface IVendedorRepository
{
    void CadastrarVendedor(Vendedor vendedor);
    List<Vendedor> ListarVendedores();
    void AtualizarVendedor(Vendedor vendedor);
    void RemoverVendedor(int id);
}

using Entities;

namespace Business.Interfaces;

public interface IVendedorService
{
    void CadastrarVendedor(Vendedor vendedor);
    List<Vendedor> ListarVendedores();
    void AtualizarVendedor(Vendedor vendedor);
    void RemoverVendedor(int id);
}
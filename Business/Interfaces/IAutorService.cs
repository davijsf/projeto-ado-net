using Entities;
namespace Business.Interfaces;
public interface IAutorService
{
    void CadastrarAutor(Autor autor);
    Autor ? BuscarAutorPorNome(string nome);
    List<Autor> ListarAutores();
    void AtualizarAutor(Autor autor);
    void RemoverAutor(int id);
}


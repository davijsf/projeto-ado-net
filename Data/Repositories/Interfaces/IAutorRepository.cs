namespace Data.Repositories.Interfaces;

using Entities;

public interface IAutorRepository
{
    void CadastrarAutor(Autor autor);
    List<Autor> ListarAutores();
    Autor? BuscarPorNome(string nome);
    void AtualizarAutor(Autor autor);
    void RemoverAutor(int id);
}

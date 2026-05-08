namespace Data.Repositories;

using Entities;

public interface IAutorRepository
{
    void CadastrarAutor(Autor autor);
    List<Autor> ListarAutores();
    Autor? BuscarAutorPorNome(string nome);
    void AtualizarAutor(Autor autor);
    void RemoverAutor(int id);
}

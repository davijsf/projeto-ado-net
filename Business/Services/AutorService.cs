namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;

public class AutorService : IAutorService
{
    private readonly AutorRepository _repository = new AutorRepository();

    public void CadastrarAutor(Autor autor)
    {
        _repository.CadastrarAutor(autor);
    }

    public List<Autor> ListarAutores()
    {
        return _repository.ListarAutores();
    }

    public void AtualizarAutor(Autor autor)
    {
        _repository.AtualizarAutor(autor);
    }

    public void RemoverAutor(int id)
    {
        _repository.RemoverAutor(id);
    }

    public Autor? BuscarPorNome(string nome)
    {
        return _repository.BuscarPorNome(nome);
    }
}

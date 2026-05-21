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
        var autorExistente = _repository.BuscarPorId(autor.Id);

        if (autorExistente == null)
            throw new InvalidOperationException("Autor não encontrado.");

        // autualização parcial
        autorExistente.Nome = string.IsNullOrWhiteSpace(autor.Nome)
            ? autorExistente.Nome
            : autor.Nome;

        autorExistente.Nacionalidade = string.IsNullOrWhiteSpace(autor.Nacionalidade)
            ? autorExistente.Nacionalidade
            : autor.Nacionalidade;
        
        _repository.AtualizarAutor(autorExistente);
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

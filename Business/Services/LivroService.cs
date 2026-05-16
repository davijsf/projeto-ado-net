namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;

public class LivroService : ILivroService
{
    private readonly LivroRepository _repository = new LivroRepository();

    public void CadastrarLivro(Livro livro)
    {
        _repository.CadastrarLivro(livro);
    }

    public List<Livro> ListarLivros()
    {
        return _repository.ListarLivros();
    }

    public List<Livro> ConsultarLivrosPorAutor(int autorId)
    {
        return _repository.ConsultarLivrosPorAutor(autorId);
    }

    public void AtualizarLivro(Livro livro)
    {
        _repository.AtualizarLivro(livro);
    }

    public void RemoverLivro(int id)
    {
        _repository.RemoverLivro(id);
    }
    public Livro? ConsultarLivroPorId(int id)
    {
        return _repository.ConsultarLivroPorId(id);
    }
}

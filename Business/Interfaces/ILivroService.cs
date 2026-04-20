using Entities;

namespace Business.Interfaces;

public interface ILivroService
{
    void CadastrarLivro(Livro livro);
    List<Livro> ListarLivros();
    void AtualizarLivro(Livro livro);
    void RemoverLivro(int id);
    List<Livro> ConsultarLivrosPorAutor(int autorId);
}
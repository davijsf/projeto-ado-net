namespace Data.Repositories.Interfaces;

using Entities;

public interface ILivroRepository
{
    void CadastrarLivro(Livro livro);
    List<Livro> ListarLivros();
    void AtualizarLivro(Livro livro);
    void RemoverLivro(int id);
    List<Livro> ConsultarLivrosPorAutor(int autorId);
    public Livro? ConsultarLivroPorId(int id);
}

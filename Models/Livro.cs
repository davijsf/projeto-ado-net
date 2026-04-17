namespace Models;

public class Livro
{
    public int Id { get ; set ;}
    public string ? Titulo { get ; set ;}
    public double Preco { get ; set ;}
    public int Estoque { get ; set ;}

    // Chave estrangeira
    public int? IdAutor { get ; set ;}
    // propriedade de navegação (se quiser saber o Autor.nome...)
    public Autor? Autor { get ; set ;}
}
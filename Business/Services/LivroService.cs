namespace Services;

using Business.Interfaces;
using Entities;
using Data;
using System.Data;

public class LivroService : ILivroService
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    public void CadastrarLivro(Livro livro)
    {
        string sql = """
            INSERT INTO livro (titulo, preco, estoque, id_autor)
            VALUES (@titulo, @preco, @estoque, @id_autor)
            """;
        
        var parametros = new Dictionary<string, object>
        {
            { "@titulo", livro.Titulo! },
            { "@preco", livro.Preco },
            {"@estoque", livro.Estoque },
            { "@id_autor", livro.IdAutor! }
        };

        _db.ExecutarComando(sql, parametros);
    }

    public List<Livro> ListarLivros()
    {
        string sql = "SELECT * FROM livro";

        DataTable dt = _db.PreencherTabela(sql);

        return dt.AsEnumerable()
            .Select(row => new Livro
            {
                Id = row.Field<int>("id"),
                Titulo = row.Field<string>("titulo"),
                Preco = row.Field<double>("preco"),
                Estoque = row.Field<int>("estoque"),
                IdAutor = row.Field<int>("id_autor")
            })
            .ToList();
    }

    public List<Livro> ConsultarLivrosPorAutor(int autorId)
    {
        string sql = @"SELECT l. * FROM Livro l WHERE l.id_autor =  @autorId";

        DataTable dt = _db.PreencherTabela(sql);

        var parametros = new Dictionary<string, object>
        {
            { "@idAutor", autorId }
        };

        dt = _db.PreencherTabela(sql, parametros);

        return dt.AsEnumerable()
        .Select(row => new Livro
        {
            Id = row.Field<int>("id"), 
            Titulo = row.Field<string>("titulo") ?? string.Empty,
            Preco = row.Field<double>("preco"),
            Estoque = row.Field<int>("estoque"),
            IdAutor = row.Field<int>("id_autor"), 
        })
        .ToList();
    }


    public void AtualizarLivro(Livro livro)
    {
        string sql = "UPDATE livro SET titulo = @titulo, estoque = @estoque, preco = @preco, id_autor = @idAutor WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", livro.Id },
            { "@titulo", livro.Titulo! },
            { "@estoque", livro.Estoque },
            { "@preco", livro.Preco },
            { "@id_autor", livro.IdAutor! }
        };  

        _db.ExecutarComando(sql, parametros);

    }

    public void RemoverLivro(int id)
    {
        string sql = "DELETE FROM livro WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }  
        };
          
        _db.ExecutarComando(sql, parametros);
    }

}
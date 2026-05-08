namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;

public class LivroRepository : RepositoryBase
{
    public void CadastrarLivro(Livro livro)
    {
        const string sql = "INSERT INTO livro (titulo, preco, estoque, id_autor) VALUES (@titulo, @preco, @estoque, @id_autor)";
        var parametros = new Dictionary<string, object>
        {
            { "@titulo", livro.Titulo! },
            { "@preco", livro.Preco },
            { "@estoque", livro.Estoque },
            { "@id_autor", livro.IdAutor! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public List<Livro> ListarLivros()
    {
        const string sql = "SELECT id, titulo, preco, estoque, id_autor FROM livro";
        DataTable dt = ExecuteTable(sql);

        return dt.AsEnumerable()
            .Select(row => new Livro
            {
                Id = row.Field<int>("id"),
                Titulo = row.Field<string>("titulo"),
                Preco = row.Field<decimal>("preco"),
                Estoque = row.Field<int>("estoque"),
                IdAutor = row.Field<int>("id_autor")
            })
            .ToList();
    }

    public List<Livro> ConsultarLivrosPorAutor(int autorId)
    {
        const string sql = "SELECT id, titulo, preco, estoque, id_autor FROM livro WHERE id_autor = @autorId";
        var parametros = new Dictionary<string, object>
        {
            { "@autorId", autorId }
        };

        DataTable dt = ExecuteTable(sql, parametros);
        return dt.AsEnumerable()
            .Select(row => new Livro
            {
                Id = row.Field<int>("id"),
                Titulo = row.Field<string>("titulo"),
                Preco = row.Field<decimal>("preco"),
                Estoque = row.Field<int>("estoque"),
                IdAutor = row.Field<int>("id_autor")
            })
            .ToList();
    }

    public void AtualizarLivro(Livro livro)
    {
        const string sql = "UPDATE livro SET titulo = @titulo, estoque = @estoque, preco = @preco, id_autor = @idAutor WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", livro.Id },
            { "@titulo", livro.Titulo! },
            { "@estoque", livro.Estoque },
            { "@preco", livro.Preco },
            { "@idAutor", livro.IdAutor! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void RemoverLivro(int id)
    {
        const string sql = "DELETE FROM livro WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        ExecuteNonQuery(sql, parametros);
    }
}

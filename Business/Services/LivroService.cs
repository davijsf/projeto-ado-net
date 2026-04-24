namespace Services;

using Data;
using System.Data;
using Business.Interfaces;
using Entities;
using MySqlConnector;

public class LivroService : ILivroService
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    //refatorado
    public void CadastrarLivro(Livro livro)
    {
        //rever essa parte, metodo BuscarAutorPorNome não esta funcionando
        AutorService autorService = new AutorService();
        var autor = autorService.BuscarAutorPorNome(nomeAutor);
        if (autor == null)
        {
            Console.WriteLine("Autor não encontrado.");
            return;
        }

        string sql = "INSERT INTO livro (titulo, preco, estoque, id_autor)"+
                     "VALUES (@titulo, @preco, @estoque, @id_autor)";

        var parametros = new Dictionary<string, object>
        {
            { "@titulo", livro.Titulo! },
            { "@preco", livro.Preco! },
            { "@estoque", livro.Estoque! },
            { "@idAutor", livro.IdAutor! }
        };

        _db.ExecutarComando(sql, parametros);

    }

    //refatorado
    public List<Livro> ListarLivros()
    {
        string sql = "SELECT * FROM livro";

            DataTable dt = _db.PreencherTabela(sql);

            List<Livro> livros = new List<Livro>();

            foreach (DataRow row in dt.Rows)
            {
                livros.Add(new Livro
                {
                    Id    = Convert.ToInt32(row["id"]),
                    Titulo      = row["titulo"].ToString(),
                    Preco = Convert.ToDouble(row["preco"]),
                    Estoque    = Convert.ToInt32(row["estoque"]),
                    IdAutor = row["id_autor"] == DBNull.Value
                                ? null
                                : Convert.ToInt32(row["id_autor"])
                });
            }

        return livros;
    }

    //refatorado
    public List<Livro> ConsultarLivrosPorAutor(int id)
    {
        string sql = "SELECT * FROM livro AS l JOIN autor AS a ON l.id = a.id WHERE a.id = @idAutor";

        DataTable dt = _db.PreencherTabela(sql);

        List<Livro> livros = new List<Livro>();

        foreach (DataRow row in dt.Rows)
        {
            livros.Add(new Livro
            {
                Id    = Convert.ToInt32(row["id"]),
                Titulo      = row["titulo"].ToString(),
                Preco = Convert.ToDouble(row["preco"]),
                Estoque    = Convert.ToInt32(row["estoque"]),
                IdAutor = row["id_autor"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(row["id_autor"])
            });
        }

        return livros;
    }

    //refatorado
    public void AtualizarLivro(Livro livro)
    {
        string sql = "UPDATE livro SET titulo = @titulo, preco = @preco, " +
                     "estoque = @estoque, id_autor = @idAutor" +
                     "WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id",    livro.Id },
            { "@titulo",      livro.Titulo! },
            { "@preco", livro.Preco! },
            { "@estoque",   livro.Estoque! },
            { "@idAutor",   livro.IdAutor! }
            
        };

        _db.ExecutarComando(sql, parametros);
    }

    //rever, talvez o parametro não esteja correto
    public void AtualizarPrecoLivro(Livro livro)
    {
        string sql = "UPDATE livro SET preco = @preco WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id",    livro.Id },
            { "@preco", livro.Preco! }
        };

        _db.ExecutarComando(sql, parametros);
        
    }

    //refatorado
    public void RemoverLivro(int id)
    {
        string sql = "DELETE FROM vendedor WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        _db.ExecutarComando(sql, parametros);
    }

}
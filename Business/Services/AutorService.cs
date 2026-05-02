namespace Services;
using Data;
using System.Data;
using Business.Interfaces;
using Entities;

public class AutorService : IAutorService
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    public void CadastrarAutor(Autor autor)
    {
         string sql = "INSERT INTO vendedor (nome, nacionalidade) " +
                     "VALUES (@nome, @nacionalidade)";

        var parametros = new Dictionary<string, object>
        {
            { "@nome",      autor.Nome! },
            { "@matricula", autor.Nacionalidade! },
        };

        _db.ExecutarComando(sql, parametros);
    }

    public List<Autor> ListarAutores()
    {
        string sql = "SELECT id, nome, nacionalidade, id- FROM autor";

        DataTable dt = _db.PreencherTabela(sql);
        
        return dt.AsEnumerable()
            .Select(row => new Autor
            {
                Id = row.Field<int>("id"),
                Nome = row.Field<string>("nome"),
                Nacionalidade = row.Field<string>("nacionalidade")
            })
            .ToList();
    }

    public Autor ? BuscarAutorPorNome(string nome)
    {
        string sql = "SELECT * FROM autor WHERE nome = @nome";
        
        var parametros = new Dictionary<string, object>
        {
            {"nome", nome}
        };

        DataTable dt = _db.PreencherTabela(sql, parametros);

        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            return new Autor
            {
                Id = Convert.ToInt32(row["id"]),
                Nome = row["nome"].ToString(),
                Nacionalidade = row["nacionalidade"].ToString()
            };
        }
        return null;
    }

    public void AtualizarAutor(Autor autor)
    {
        string sql = "UPDATE vendedor SET nome = @nome, nacionalidade = @nacionalidade" +
                     "WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", autor.Id },
            { "@nome", autor.Nome! },
            { "@nacionalidade", autor.Nacionalidade! },
        };

        _db.ExecutarComando(sql, parametros);
    }

    public void RemoverAutor(int id)
    {
        string sql = "DELETE FROM autor WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        _db.ExecutarComando(sql, parametros);
    }
}
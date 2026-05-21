namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;

public class AutorRepository : RepositoryBase
{
    public void CadastrarAutor(Autor autor)
    {
        const string sql = "INSERT INTO autor (nome, nacionalidade) VALUES (@nome, @nacionalidade)";
        var parametros = new Dictionary<string, object>
        {
            { "@nome", autor.Nome! },
            { "@nacionalidade", autor.Nacionalidade! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public List<Autor> ListarAutores()
    {
        const string sql = "SELECT id, nome, nacionalidade FROM autor";
        DataTable dt = ExecuteTable(sql);

        return dt.AsEnumerable()
            .Select(row => new Autor
            {
                Id = row.Field<int>("id"),
                Nome = row.Field<string>("nome"),
                Nacionalidade = row.Field<string>("nacionalidade")
            })
            .ToList();
    }

    public Autor? BuscarPorNome(string nome)
    {
        const string sql = "SELECT id, nome, nacionalidade FROM autor WHERE nome = @nome";
        var parametros = new Dictionary<string, object>
        {
            { "@nome", nome }
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        return new Autor
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Nacionalidade = row.Field<string>("nacionalidade")
        };
    }

    public Autor? BuscarPorId(int id)
    {
        const string sql = "SELECT id, nome, nacionalidade FROM autor WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        return new Autor
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Nacionalidade = row.Field<string>("nacionalidade")
        };
    }

    public void AtualizarAutor(Autor autor)
    {
        const string sql = "UPDATE autor SET nome = @nome, nacionalidade = @nacionalidade WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", autor.Id },
            { "@nome", autor.Nome! },
            { "@nacionalidade", autor.Nacionalidade! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void RemoverAutor(int id)
    {
        const string sql = "DELETE FROM autor WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        ExecuteNonQuery(sql, parametros);
    }
}

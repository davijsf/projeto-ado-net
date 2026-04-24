namespace Services;
using Data;
using System.Data;
using Business.Interfaces;
using Entities;
using MySqlConnector;

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

        List<Autor> autores = new List<Autor>();

        foreach (DataRow row in dt.Rows)
        {
            autores.Add(new Autor
            {
                Id    = Convert.ToInt32(row["id_vend"]),
                Nome      = row["nome"].ToString(),
                Nacionalidade = row["nacionalidade"].ToString()
            });
        }

        return autores;
    }


    /*precisa refatorar ainda
    public Autor ? BuscarAutorPorNome(string nome)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string sql = "SELECT * FROM autor WHERE nome = @nome";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@nome", nome);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Autor
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Nacionalidade = reader.GetString("nacionalidade")
            };
        }
        return null;
    }*/

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
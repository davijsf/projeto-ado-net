namespace Services;

using Entities;
using MySqlConnector;

public class AutorService
{
    private readonly string _connectionString = 
                        "server=localhost;" +
                       "database=livraria_ado_net;" +
                       "uid=root;" +
                       "pwd=1234";

    public void CadastrarAutor(string nome, string nacionalidade)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO autor (nome, nacionalidade) VALUES (@nome, @nacionalidade)";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@nacionalidade", nacionalidade);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<Autor> ListarAutores()
    {
        List<Autor> autores = new List<Autor>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT id, nome, nacionalidade FROM autor";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Autor autor = new Autor
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Nacionalidade = reader.IsDBNull(reader.GetOrdinal("nacionalidade")) ? null : reader.GetString("nacionalidade")
                        };
                        autores.Add(autor);
                    }
                }
            }
        }
        return autores;
    }

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
    }

    public void AtualizarAutor(int id, string nome, string nacionalidade)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "UPDATE autor SET nome = @nome, nacionalidade = @nacionalidade WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@nacionalidade", nacionalidade);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void DeletarAutor(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM autor WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
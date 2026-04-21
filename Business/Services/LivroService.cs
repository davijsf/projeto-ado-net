namespace Services;

using Entities;
using MySqlConnector;

public class LivroService
{
    private readonly string _connectionString =  
                       "server=localhost;" +
                       "database=livraria_ado_net;" +
                       "uid=root;" +
                       "pwd=1234";

    public void CadastrarLivro(string titulo, double preco, int estoque, string nomeAutor)
    {
        AutorService autorService = new AutorService();
        var autor = autorService.BuscarAutorPorNome(nomeAutor);

        if (autor == null)
        {
            Console.WriteLine("Autor não encontrado.");
            return;
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string sql = "INSERT INTO livro (titulo, preco, estoque, id_autor) VALUES (@titulo, @preco, @estoque, @id_autor)";

        using (MySqlCommand cmd = new MySqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@titulo", titulo);
            cmd.Parameters.AddWithValue("@preco", preco);
            cmd.Parameters.AddWithValue("@estoque", estoque);
            cmd.Parameters.AddWithValue("@id_autor", autor.Id);

            cmd.ExecuteNonQuery();
        }
    }

    public List<Livro> ListarLivros()
    {
        List<Livro> livros = new List<Livro>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM livro";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Livro livro = new Livro
                        {
                            Id = reader.GetInt32("id"),
                            Titulo = reader.GetString("titulo"),
                            Preco = reader.GetDouble("preco"),
                            Estoque = reader.GetInt32("estoque"),
                            IdAutor = reader.GetInt32("id_autor")
                        };
                        livros.Add(livro);
                    }
                }
            }
        }
        return livros;
    }

    public List<Livro> BuscarLivroPorAutor(string nomeAutor)
    {
        List<Livro> livros = new List<Livro>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM livro AS l JOIN autor AS a ON l.id_autor = a.id WHERE a.nome = @nomeAutor";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@nomeAutor", nomeAutor);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Livro livro = new Livro
                        {
                            Id = reader.GetInt32("id"),
                            Titulo = reader.GetString("titulo"),
                            Preco = reader.GetDouble("preco"),
                            Estoque = reader.GetInt32("estoque"),
                            IdAutor = reader.GetInt32("id_autor")
                        };
                        livros.Add(livro);
                    }
                }
            }
        }
        return livros;
    }


    public void AtualizarPrecoLivro(int id, double NovoPreco)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "UPDATE livro SET preco = @preco WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@preco", NovoPreco);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void DeletarLivro(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM livro WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }

}
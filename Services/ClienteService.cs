namespace Services;

using Models;
using ConsoleApp;

using MySqlConnector;

public class ClienteService
{

    private readonly string _connectionString = 
                        "server=localhost;" +
                       "database=livraria_ado_net;" +
                       "uid=root;" +
                       "pwd=Mateus84+";

    public void CadastrarCliente(string nome, string cpf, string email,
                                    string username, string senha, string ? avatar)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string sqlUsuario = """
                INSERT INTO usuario (username, senha, nivel, avatar) 
                VALUES (@username, @senha, 'comum', @avatar);
                """;

            int idUsuario;


            using (MySqlCommand cmd= new MySqlCommand(sqlUsuario, connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@senha", BCrypt.Net.BCrypt.HashPassword(senha));
                cmd.Parameters.AddWithValue("@avatar", (object?)email ?? DBNull.Value);
                cmd.ExecuteNonQuery();

                idUsuario = (int)cmd.LastInsertedId;  // ← pega o id gerado
            }

            string sqlCliente = "INSERT INTO cliente (nome, cpf, email) VALUES (@nome, @cpf, @email)";

            using (MySqlCommand cmd = new MySqlCommand(sqlCliente, connection))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@email", (object?)email ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public Cliente? ListarClientePorId(int id)
    {

        using (DatabaseConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = $"SELECT * FROM cliente WHERE cliente.id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientes.Add( new Cliente
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Cpf = reader.GetString("cpf"),
                        Email = reader.GetString("email"),
                    });
                }
            }
        }

        return null;
    }



    
}
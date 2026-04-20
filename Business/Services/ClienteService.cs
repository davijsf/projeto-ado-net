namespace Services;

using Entities;
using MySqlConnector;

public class ClienteService
{
    private readonly string _connectionString = 
        "server=localhost;" +
        "database=livraria_ado_net;" +
        "uid=root;" +
        "pwd=1234";

    public void CadastrarCliente(string nome, string cpf, string email,
                                string username, string senha, string nivel, string avatar)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        // // Primeiro usuário
        UsuarioService usuarioService = new UsuarioService();
        int idUsuario = usuarioService.CadastrarUsuario(username, senha, nivel, avatar);


        // Depois cliente
        string sqlCliente = "INSERT INTO cliente (nome, cpf, email, usuario_id) VALUES (@nome, @cpf, @email, @usuario_id);";
        using var cmdCliente = new MySqlCommand(sqlCliente, connection);
        cmdCliente.Parameters.AddWithValue("@nome", nome);
        cmdCliente.Parameters.AddWithValue("@cpf", cpf);
        cmdCliente.Parameters.AddWithValue("@email", (object?)email ?? DBNull.Value);
        cmdCliente.Parameters.AddWithValue("@usuario_id", idUsuario);
        cmdCliente.ExecuteNonQuery();
    }

    public List<Cliente> ListarClientes()
    {
        var clientes = new List<Cliente>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string sql = "SELECT * FROM cliente";
        using var cmd = new MySqlCommand(sql, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            clientes.Add(new Cliente
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Cpf = reader.GetString("cpf"),
                Email = reader.GetString("email")
            });
        }

        return clientes;
    }

    public Cliente? ListarClientePorId(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string sql = "SELECT * FROM cliente WHERE id = @id";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Cliente
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Cpf = reader.GetString("cpf"),
                Email = reader.GetString("email")
            };
        }
        return null;
    }

    public void AtualizarEmailCliente(int id, string NovoEmail)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string sql = "UPDATE cliente SET email = @novoEmail";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@email", NovoEmail);

        cmd.ExecuteNonQuery();
    }

    public void DeletarCliente(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM cliente WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
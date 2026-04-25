namespace Services;

using Data;
using System.Data;
using Entities;
using MySqlConnector;
using Business.Interfaces;

public class ClienteService : IClienteServices
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    //refatorado com problemas 
    public void CadastrarCliente(Cliente cliente)
    {
        // // Primeiro usuário
        UsuarioService usuarioService = new UsuarioService();
        int idUsuario = usuarioService.CadastrarUsuario(username, senha, nivel, avatar);

        // Depois cliente
        string sqlCliente = "INSERT INTO cliente (nome, cpf, email, id_usuario) VALUES (@nome, @cpf, @email, @id_usuario);";

        var parametros = new Dictionary<string, object>
        {
            { "@nome",      cliente.Nome! },
            { "@cpf", cliente.Cpf! },
            { "@email",   cliente.Email! },
            { "@id_usuario", cliente.IdUsuario! } //tem que adicionar usuario em cliente no banco 
        };

        _db.ExecutarComando(sql, parametros);
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

    public void AtualizarCliente(Cliente cliente)
    {
        
    }
    public void AtualizarEmailCliente(int id, string NovoEmail)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string sql = "UPDATE cliente SET email = @novoEmail WHERE id = @id";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@novoEmail", NovoEmail);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public void RemoverCliente(int id)
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
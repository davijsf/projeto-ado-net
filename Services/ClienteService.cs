namespace Services;

using System.Data.SqlTypes;
using System.Security;
using Models;

using MySqlConnector;

public class ClienteService
{

    private readonly string _connectionString = 
                        "server=localhost;" +
                       "database=livraria_ado_net;" +
                       "uid=root;" +
                       "pwd=1234";

    public void CadastrarCliente(string nome, string cpf, string email)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string sql = "INSERT INTO cliente (nome, cpf, email) VALUES (@nome, @cpf, @email)";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
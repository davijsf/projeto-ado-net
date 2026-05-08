namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;

public class ClienteRepository : RepositoryBase
{
    public void CadastrarCliente(Cliente cliente)
    {
        const string sql = "INSERT INTO cliente (nome, cpf, email) VALUES (@nome, @cpf, @email)";
        var parametros = new Dictionary<string, object>
        {
            { "@nome", cliente.Nome! },
            { "@cpf", cliente.Cpf! },
            { "@email", cliente.Email! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public List<Cliente> ListarClientes()
    {
        const string sql = "SELECT id, id_usuario, nome, cpf, email FROM cliente";
        DataTable dt = ExecuteTable(sql);

        return dt.AsEnumerable()
            .Select(row => new Cliente
            {
                IdClient = row.Field<int>("id"),
                Id = row.Field<int>("id_usuario"),
                Nome = row.Field<string>("nome"),
                Cpf = row.Field<string>("cpf"),
                Email = row.Field<string>("email")
            })
            .ToList();
    }

    public void AtualizarCliente(Cliente cliente)
    {
        const string sql = "UPDATE cliente SET nome = @nome, cpf = @cpf, email = @email WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", cliente.IdClient },
            { "@nome", cliente.Nome! },
            { "@cpf", cliente.Cpf! },
            { "@email", cliente.Email! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void RemoverCliente(int id)
    {
        const string sql = "DELETE FROM cliente WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        ExecuteNonQuery(sql, parametros);
    }
}

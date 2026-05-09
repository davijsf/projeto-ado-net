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

    public Cliente ? BuscarPorCpf(string cpf)
    {
        const string sql = "SELECT * FROM cliente WHERE cpf = @cpf";
        var parametros = new Dictionary<string, object>
        {
            {"@cpf", cpf}
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        return new Cliente
        {
            Cpf = row.Field<string>("cpf")
        };
    }

    public Cliente ? BuscarPorId(int id)
    {
        const string sql = "SELECT * FROM cliente WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            {"@id", id}
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        return new Cliente
        {
            Id = row.Field<int>("id")
        };
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

namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;
using Interfaces;

public class VendedorRepository : RepositoryBase, IVendedorRepository
{
    public void CadastrarVendedor(Vendedor vendedor)
    {
        const string sql =
            "INSERT INTO vendedor (nome, matricula, salario, id_usuario) " +
            "VALUES (@nome, @matricula, @salario, @idUsuario)";

        var parametros = new Dictionary<string, object>
        {
            { "@nome", vendedor.Nome! },
            { "@matricula", vendedor.Matricula! },
            { "@salario", vendedor.Salario },
            { "@idUsuario", vendedor.IdUsuario! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public List<Vendedor> ListarVendedores()
    {
        const string sql =
            "SELECT id, nome, matricula, salario, id_usuario FROM vendedor";

        DataTable dt = ExecuteTable(sql);

        return dt.AsEnumerable()
            .Select(row => new Vendedor
            {
                IdVend = row.Field<int>("id"),
                Nome = row.Field<string>("nome"),
                Matricula = row.Field<string>("matricula"),
                Salario = Convert.ToDouble(row.Field<decimal>("salario")),
                IdUsuario = row.IsNull("id_usuario")
                    ? null
                    : row.Field<int>("id_usuario")
            })
            .ToList();
    }

    public Vendedor? BuscarPorId(int id)
    {
        const string sql =
            "SELECT id, nome, matricula, salario, id_usuario " +
            "FROM vendedor WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        DataTable dt = ExecuteTable(sql, parametros);

        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];

        return new Vendedor
        {
            IdVend = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Matricula = row.Field<string>("matricula"),
            Salario = Convert.ToDouble(row.Field<decimal>("salario")),
            IdUsuario = row.IsNull("id_usuario")
                ? null
                : row.Field<int>("id_usuario")
        };
    }

    public Vendedor? BuscarPorMatricula(string matricula)
    {
        const string sql =
            "SELECT id, nome, matricula, salario, id_usuario " +
            "FROM vendedor WHERE matricula = @matricula";

        var parametros = new Dictionary<string, object>
        {
            { "@matricula", matricula }
        };

        DataTable dt = ExecuteTable(sql, parametros);

        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];

        return new Vendedor
        {
            IdVend = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Matricula = row.Field<string>("matricula"),
            Salario = Convert.ToDouble(row.Field<decimal>("salario")),
            IdUsuario = row.IsNull("id_usuario")
                ? null
                : row.Field<int>("id_usuario")
        };
    }

    public void AtualizarVendedor(Vendedor vendedor)
    {
        const string sql =
            "UPDATE vendedor " +
            "SET nome = @nome, matricula = @matricula, " +
            "salario = @salario, id_usuario = @idUsuario " +
            "WHERE id = @idVend";

        var parametros = new Dictionary<string, object>
        {
            { "@idVend", vendedor.IdVend },
            { "@nome", vendedor.Nome! },
            { "@matricula", vendedor.Matricula! },
            { "@salario", vendedor.Salario },
            { "@idUsuario", vendedor.IdUsuario! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void RemoverVendedor(int id)
    {
        const string sql =
            "DELETE FROM vendedor WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        ExecuteNonQuery(sql, parametros);
    }
}
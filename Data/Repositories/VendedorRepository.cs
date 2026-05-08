namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;

public class VendedorRepository : RepositoryBase, IVendedorRepository
{
    public void CadastrarVendedor(Vendedor vendedor)
    {
        const string sql = "INSERT INTO vendedor (nome, matricula, salario, id_usuario) VALUES (@nome, @matricula, @salario, @idUsuario)";
        var parametros = new Dictionary<string, object>
        {
            { "@nome", vendedor.Nome! },
            { "@matricula", vendedor.Matricula! },
            { "@salario", vendedor.Salario },
            { "@idUsuario", vendedor.IdUsuario ?? DBNull.Value }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public List<Vendedor> ListarVendedores()
    {
        const string sql = "SELECT id_vend, nome, matricula, salario, id_usuario FROM vendedor";
        DataTable dt = ExecuteTable(sql);

        return dt.AsEnumerable()
            .Select(row => new Vendedor
            {
                IdVend = row.Field<int>("id_vend"),
                Nome = row.Field<string>("nome"),
                Matricula = row.Field<string>("matricula"),
                Salario = Convert.ToDouble(row.Field<decimal>("salario")),
                IdUsuario = row.IsNull("id_usuario") ? null : row.Field<int>("id_usuario")
            })
            .ToList();
    }

    public void AtualizarVendedor(Vendedor vendedor)
    {
        const string sql = "UPDATE vendedor SET nome = @nome, matricula = @matricula, salario = @salario, id_usuario = @idUsuario WHERE id_vend = @idVend";
        var parametros = new Dictionary<string, object>
        {
            { "@idVend", vendedor.IdVend },
            { "@nome", vendedor.Nome! },
            { "@matricula", vendedor.Matricula! },
            { "@salario", vendedor.Salario },
            { "@idUsuario", vendedor.IdUsuario ?? DBNull.Value }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void RemoverVendedor(int id)
    {
        const string sql = "DELETE FROM vendedor WHERE id_vend = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        ExecuteNonQuery(sql, parametros);
    }
}

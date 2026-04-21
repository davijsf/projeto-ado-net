namespace Services;

using Business.Interfaces;
using Data;
using Entities;
using System.Data;

public class VendedorService : IVendedorService
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    public void CadastrarVendedor(Vendedor vendedor)
    {
        string sql = "INSERT INTO vendedor (nome, matricula, salario, id_usuario) " +
                     "VALUES (@nome, @matricula, @salario, @idUsuario)";

        var parametros = new Dictionary<string, object>
        {
            { "@nome",      vendedor.Nome! },
            { "@matricula", vendedor.Matricula! },
            { "@salario",   vendedor.Salario },
            { "@idUsuario", vendedor.IdUsuario! }
        };

        _db.ExecutarComando(sql, parametros);
    }

    public List<Vendedor> ListarVendedores()
    {
        string sql = "SELECT id_vend, nome, matricula, salario, id_usuario FROM vendedor";

        DataTable dt = _db.PreencherTabela(sql);

        List<Vendedor> vendedores = new List<Vendedor>();

        foreach (DataRow row in dt.Rows)
        {
            vendedores.Add(new Vendedor
            {
                IdVend    = Convert.ToInt32(row["id_vend"]),
                Nome      = row["nome"].ToString(),
                Matricula = row["matricula"].ToString(),
                Salario   = Convert.ToDouble(row["salario"]),
                IdUsuario = row["id_usuario"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(row["id_usuario"])
            });
        }

        return vendedores;
    }

    public void AtualizarVendedor(Vendedor vendedor)
    {
        string sql = "UPDATE vendedor SET nome = @nome, matricula = @matricula, " +
                     "salario = @salario, id_usuario = @idUsuario " +
                     "WHERE id_vend = @idVend";

        var parametros = new Dictionary<string, object>
        {
            { "@idVend",    vendedor.IdVend },
            { "@nome",      vendedor.Nome! },
            { "@matricula", vendedor.Matricula! },
            { "@salario",   vendedor.Salario },
            { "@idUsuario", vendedor.IdUsuario! }
        };

        _db.ExecutarComando(sql, parametros);
    }

    public void RemoverVendedor(int id)
    {
        string sql = "DELETE FROM vendedor WHERE id_vend = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        _db.ExecutarComando(sql, parametros);
    }
}
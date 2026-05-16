namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;

public class VendaRepository : RepositoryBase
{
    public int RegistrarVenda(Venda venda)
    {
        const string sql = "INSERT INTO venda (data, total, id_cliente, id_vendedor) VALUES (@data, @total, @id_cliente, @id_vendedor); SELECT LAST_INSERT_ID();";
        var parametros = new Dictionary<string, object>
        {
            { "@data", venda.DataVenda },
            { "@total", venda.Total },
            { "@id_cliente", venda.IdCliente },
            { "@id_vendedor", venda.IdVendedor }
        };

        object? result = ExecuteScalar(sql, parametros);
        return Convert.ToInt32(result);
    }

    public void AdicionarItem(int vendaId, ItemVenda item)
    {
        const string sqlPreco = "SELECT preco FROM livro WHERE id = @id_livro";
        var precoTable = ExecuteTable(sqlPreco, new Dictionary<string, object> { { "@id_livro", item.IdLivro } });
        if (precoTable.Rows.Count == 0)
            throw new Exception("Livro não encontrado");

        decimal preco = Convert.ToDecimal(precoTable.Rows[0]["preco"]);
        item.SubTotal = (decimal)(preco * item.Quantidade);

        const string sqlItem = "INSERT INTO itemvenda (quantidade, subtotal, id_livro, id_venda) VALUES (@quantidade, @subtotal, @id_livro, @id_venda)";
        var parametros = new Dictionary<string, object>
        {
            { "@quantidade", item.Quantidade },
            { "@subtotal", item.SubTotal },
            { "@id_livro", item.IdLivro },
            { "@id_venda", vendaId }
        };

        ExecuteNonQuery(sqlItem, parametros);
    }

    public void RemoverItem(int vendaId, int itemId)
    {
        const string sql = "DELETE FROM itemvenda WHERE id = @id AND id_venda = @id_venda";
        var parametros = new Dictionary<string, object>
        {
            { "@id", itemId },
            { "@id_venda", vendaId }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public decimal CalcularTotal(int vendaId)
    {
        const string sql = "SELECT SUM(subtotal) FROM itemvenda WHERE id_venda = @id_venda";
        var dt = ExecuteTable(sql, new Dictionary<string, object> { { "@id_venda", vendaId } });
        if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            return Convert.ToDecimal(dt.Rows[0][0]);
        return 0;
    }

    public List<Venda> ListarVendas()
    {
        const string sql = @"
            SELECT v.id, v.data, v.total, v.id_cliente, v.id_vendedor,
                   c.nome AS cliente_nome, ven.nome AS vendedor_nome
            FROM venda v
            JOIN cliente c ON v.id_cliente = c.id
            JOIN vendedor ven ON v.id_vendedor = ven.id";

        DataTable dt = ExecuteTable(sql);
        return dt.AsEnumerable()
            .Select(row => new Venda
            {
                Id = Convert.ToInt32(row["id"]),
                DataVenda = Convert.ToDateTime(row["data"]),
                Total = Convert.ToDouble(row["total"]),
                IdCliente = Convert.ToInt32(row["id_cliente"]),
                IdVendedor = Convert.ToInt32(row["id_vendedor"]),
                Cliente = new Cliente { Nome = row["cliente_nome"].ToString() },
                Vendedor = new Vendedor { Nome = row["vendedor_nome"].ToString() }
            })
            .ToList();
    }

    public List<Venda> ConsultarVendasPorCliente(int clienteId)
    {
        const string sql = @"
            SELECT v.id, v.data, v.total, v.id_cliente, v.id_vendedor,
                   c.nome AS cliente_nome, ven.nome AS vendedor_nome
            FROM venda v
            JOIN cliente c ON v.id_cliente = c.id
            JOIN vendedor ven ON v.id_vendedor = ven.id
            WHERE v.id_cliente = @clienteId";

        DataTable dt = ExecuteTable(sql, new Dictionary<string, object> { { "@clienteId", clienteId } });
        return dt.AsEnumerable()
            .Select(row => new Venda
            {
                Id = Convert.ToInt32(row["id"]),
                DataVenda = Convert.ToDateTime(row["data"]),
                Total = Convert.ToDouble(row["total"]),
                IdCliente = Convert.ToInt32(row["id_cliente"]),
                IdVendedor = Convert.ToInt32(row["id_vendedor"]),
                Cliente = new Cliente { Nome = row["cliente_nome"].ToString() },
                Vendedor = new Vendedor { Nome = row["vendedor_nome"].ToString() }
            })
            .ToList();
    }
}

using Entities;
using Business.Interfaces;
using Data;
using MySqlConnector;
using System.Data;

namespace Services;

public class VendaService 
{
    private readonly DataBaseConnection _dbConnection;

    public VendaService()
    {
        _dbConnection = new DataBaseConnection();
    }

    public void RegistrarVenda(Venda venda)
    {
        // Inserir venda
        string sqlVenda = "INSERT INTO venda (data, total, id_cliente, id_vendedor) VALUES (@data, @total, @id_cliente, @id_vendedor); SELECT LAST_INSERT_ID();";
        var parametrosVenda = new Dictionary<string, object>
        {
            { "@data", venda.Data },
            { "@total", venda.Total },
            { "@id_cliente", venda.IdCliente },
            { "@id_vendedor", venda.IdVendedor }
        };

        using (MySqlConnection conn = new MySqlConnection("server=localhost;database=livraria_ado_net;uid=root;pwd=1234;"))
        {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(sqlVenda, conn))
            {
                foreach (var p in parametrosVenda)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                venda.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }


    public void AdicionarItem(int vendaId, ItemVenda item)
    {
        // Busca preço do livro
        string sqlPreco = "SELECT preco FROM livro WHERE id = @id_livro";
        var dt = _dbConnection.PreencherTabela(sqlPreco, new Dictionary<string, object> { { "@id_livro", item.IdLivro } });
        if (dt.Rows.Count == 0) throw new Exception("Livro não encontrado");

        decimal preco = Convert.ToDecimal(dt.Rows[0]["preco"]);
        item.SubTotal = (double)(preco * item.Quantidade);

        string sqlItem = "INSERT INTO itemvenda (quantidade, subtotal, id_livro, id_venda) VALUES (@quantidade, @subtotal, @id_livro, @id_venda)";
        var parametros = new Dictionary<string, object>
        {
            { "@quantidade", item.Quantidade },
            { "@subtotal", item.SubTotal },
            { "@id_livro", item.IdLivro },
            { "@id_venda", vendaId }
        };
        _dbConnection.ExecutarComando(sqlItem, parametros);

       
    }

    public void RemoverItem(int vendaId, int itemId)
    {
        string sql = "DELETE FROM itemvenda WHERE id = @id AND id_venda = @id_venda";
        var parametros = new Dictionary<string, object>
        {
            { "@id", itemId },
            { "@id_venda", vendaId }
        };
        _dbConnection.ExecutarComando(sql, parametros);

        
    }

    public decimal CalcularTotal(int vendaId)
    {
        string sql = "SELECT SUM(subtotal) FROM itemvenda WHERE id_venda = @id_venda";
        var dt = _dbConnection.PreencherTabela(sql, new Dictionary<string, object> { { "@id_venda", vendaId } });
        if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            return Convert.ToDecimal(dt.Rows[0][0]);
        return 0;
    }

    public List<Venda> ListarVendas()
    {
        string sql = @"
            SELECT v.id, v.data, v.total, v.id_cliente, v.id_vendedor,
                   c.nome as cliente_nome, ven.nome as vendedor_nome
            FROM venda v
            JOIN cliente c ON v.id_cliente = c.id
            JOIN vendedor ven ON v.id_vendedor = ven.id";
        var dt = _dbConnection.PreencherTabela(sql);
        var vendas = new List<Venda>();
        foreach (DataRow row in dt.Rows)
        {
            vendas.Add(new Venda
            {
                Id = Convert.ToInt32(row["id"]),
                Data = Convert.ToDateTime(row["data"]),
                Total = Convert.ToDouble(row["total"]),
                IdCliente = Convert.ToInt32(row["id_cliente"]),
                IdVendedor = Convert.ToInt32(row["id_vendedor"]),
                Cliente = new Cliente { Nome = row["cliente_nome"].ToString() },
                Vendedor = new Vendedor { Nome = row["vendedor_nome"].ToString() }
            });
        }
        return vendas;
    }

}

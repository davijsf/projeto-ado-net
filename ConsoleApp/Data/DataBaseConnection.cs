namespace Data;

using System.Data;
using MySql.Data.MySqlClient;

public class DataBaseConnection
{
    private string stConnection;

    public DataBaseConnection()
    {
        stConnection = "server=localhost;" +
                       "database=livraria_ado_net;" +
                       "uid=root;" +
                       "pwd=1234";
    }

    public void ExecutarComando(string query, Dictionary<string, object>? parametros = null)
    {
        using (MySqlConnection conn = new MySqlConnection(stConnection))
        {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parametros != null)
                    foreach (var p in parametros)
                        cmd.Parameters.AddWithValue(p.Key, p.Value);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public DataTable PreencherTabela(string query, Dictionary<string, object>? parametros = null)
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(stConnection))
        {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parametros != null)
                    foreach (var p in parametros)
                        cmd.Parameters.AddWithValue(p.Key, p.Value);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }

        return dt;
    }
}
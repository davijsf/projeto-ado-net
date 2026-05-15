namespace Data;

using System.Data;
using MySqlConnector;

public class DataBaseConnection
{
    private readonly string _stConnection;

    public DataBaseConnection()
    {
        _stConnection = "server=localhost;" +
                        "database=livraria_ado_net;" +
                        "uid=root;" +
                        "pwd=1234";
    }

    public void ExecutarComando(string query, Dictionary<string, object>? parametros = null)
    {
        using MySqlConnection conn = new(_stConnection);
        conn.Open();
        using MySqlCommand cmd = new(query, conn);
        AdicionarParametros(cmd, parametros);
        cmd.ExecuteNonQuery();
    }

    public DataTable PreencherTabela(string query, Dictionary<string, object>? parametros = null)
    {
        DataTable dt = new();

        using MySqlConnection conn = new(_stConnection);
        conn.Open();
        using MySqlCommand cmd = new(query, conn);
        AdicionarParametros(cmd, parametros);

        MySqlDataAdapter da = new(cmd);
        da.Fill(dt);

        return dt;
    }

    public object? ExecutarScalar(string query, Dictionary<string, object>? parametros = null)
    {
        using MySqlConnection conn = new(_stConnection);
        conn.Open();
        using MySqlCommand cmd = new(query, conn);
        AdicionarParametros(cmd, parametros);
        return cmd.ExecuteScalar();
    }

    private static void AdicionarParametros(MySqlCommand cmd, Dictionary<string, object>? parametros)
    {
        if (parametros == null)
            return;

        foreach (var p in parametros)
            cmd.Parameters.AddWithValue(p.Key, p.Value);
    }
}

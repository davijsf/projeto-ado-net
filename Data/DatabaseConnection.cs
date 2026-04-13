namespace Data;

using System.Data;


public class DataBase
{
    private string stConnection;

    public DataBase()
    {
        stConnection = "server=localhost;"+
        "database=db;"+
        "uid='root';"+
        "pwd='root'";
    }

    // Aqui executa: INSERT, UPDATE, DELETE
    // Parâmetros opcionais: ex: ("@NOME", "Davizera")
    public void ExecutarComando(string query, Dictionary<string, object> parametros = null )
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
        } // fecha conexão automaticamente
    }

    // Executa SELECT e retorna DataTable
    public DataTable PreencherTabela(string query, Dictionary<string, object> parametros = 0)
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
                da.Fill(tb);
            }
        }
        return dt;
    }

}
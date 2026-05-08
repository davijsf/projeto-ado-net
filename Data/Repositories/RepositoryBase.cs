namespace Data.Repositories;

using Data;
using System.Data;

public abstract class RepositoryBase
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    protected void ExecuteNonQuery(string query, Dictionary<string, object>? parametros = null)
    {
        _db.ExecutarComando(query, parametros);
    }

    protected DataTable ExecuteTable(string query, Dictionary<string, object>? parametros = null)
    {
        return _db.PreencherTabela(query, parametros);
    }

    protected object? ExecuteScalar(string query, Dictionary<string, object>? parametros = null)
    {
        return _db.ExecutarScalar(query, parametros);
    }
}

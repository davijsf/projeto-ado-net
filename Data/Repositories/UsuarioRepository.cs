namespace Data.Repositories;

using Entities;
using System.Data;
using System.Linq;

public class UsuarioRepository : RepositoryBase
{
    public Usuario? Login(string email, string senha)
    {
        const string sql = "SELECT id, username, senha, nivel, avatar FROM usuario WHERE username = @username AND senha = @senha";
        var parametros = new Dictionary<string, object>
        {
            { "@username", email },
            { "@senha", senha }
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        return new Usuario
        {
            Id = row.Field<int>("id"),
            Username = row.Field<string>("username"),
            Senha = row.Field<string>("senha"),
            nivel = row.Field<string>("nivel"),
            Avatar = row.Field<string>("avatar")!
        };
    }

    public void CadastrarUsuario(Usuario usuario)
    {
        const string sql = "INSERT INTO usuario (username, senha, nivel, avatar) VALUES (@username, @senha, @nivel, @avatar)";
        var parametros = new Dictionary<string, object>
        {
            { "@username", usuario.Username! },
            { "@senha", usuario.Senha! },
            { "@nivel", usuario.nivel! },
            { "@avatar", usuario.Avatar! }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public Usuario? BuscarUsuarioPorUsername(string username)
    {
        const string sql = "SELECT id, username, senha, nivel, avatar FROM usuario WHERE username = @username";
        var parametros = new Dictionary<string, object>
        {
            { "@username", username }
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        return new Usuario
        {
            Id = row.Field<int>("id"),
            Username = row.Field<string>("username"),
            Senha = row.Field<string>("senha"),
            nivel = row.Field<string>("nivel"),
            Avatar = row.Field<string>("avatar")!
        };
    }

    public void AlterarNivelAcesso(int usuarioId, string nivel)
    {
        const string sql = "UPDATE usuario SET nivel = @nivel WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@nivel", nivel },
            { "@id", usuarioId }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void UploadAvatar(int usuarioId, byte[] imagem)
    {
        const string sql = "UPDATE usuario SET avatar = @avatar WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@avatar", imagem },
            { "@id", usuarioId }
        };

        ExecuteNonQuery(sql, parametros);
    }
}

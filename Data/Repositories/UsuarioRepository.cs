namespace Data.Repositories;

using Entities;
using System.Data;
using BCryptNet = BCrypt.Net.BCrypt; 


public class UsuarioRepository : RepositoryBase
{
   public Usuario? Login(string email, string senha)
    {
        const string sql = "SELECT id, username, senha, nivel, avatar FROM usuario WHERE username = @username";
        var parametros = new Dictionary<string, object>
        {
            { "@username", email }
        };

        DataTable dt = ExecuteTable(sql, parametros);
        if (dt.Rows.Count == 0)
            return null;

        DataRow row = dt.Rows[0];
        string hashSalvo = row.Field<string>("senha")!;

        // Verifica a senha contra o hash armazenado
        if (!BCryptNet.Verify(senha, hashSalvo))
            return null;

        return new Usuario
        {
            Id = row.Field<int>("id"),
            Username = row.Field<string>("username")!,
            Senha = hashSalvo,
            nivel = Enum.Parse<NivelAcesso>(row.Field<string>("nivel")!, ignoreCase: true),
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
            { "@nivel", usuario.nivel!.ToString()},
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
            nivel = Enum.Parse<NivelAcesso>(row.Field<string>("nivel")!, ignoreCase: true),
            Avatar = row.Field<string>("avatar")!
        };
    }

    public void AlterarNivelAcesso(int usuarioId, NivelAcesso nivel)
    {
        const string sql = "UPDATE usuario SET nivel = @nivel WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@nivel", nivel.ToString() },
            { "@id", usuarioId }
        };

        ExecuteNonQuery(sql, parametros);
    }

    public void UploadAvatar(int usuarioId, string caminho_imagem)
    {
        const string sql = "UPDATE usuario SET avatar = @avatar WHERE id = @id";
        var parametros = new Dictionary<string, object>
        {
            { "@avatar", caminho_imagem },
            { "@id", usuarioId }
        };

        ExecuteNonQuery(sql, parametros);
    }
}

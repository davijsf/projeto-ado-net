namespace Services;

using System.Data;
using Business.Interfaces;
using Entities;
using Data;
using BCrypt.Net;

public class UsuarioService : IUsuarioService
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    public Usuario ? Login(string username, string senha)
    {
        string sql = "SELECT * FROM usuario WHERE email = @email AND senha = @senha";

        var parametros = new Dictionary<string, object>
        {
            { "email", username },
            { "senha", senha }
        };

        DataTable dt = _db.PreencherTabela(sql, parametros);

        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            return new Usuario
            {
                Id = row.Field<int>("id"),
                Username = row.Field<string>("username"),

            };
        }

        return null; // email encontrado ou senha incorreta
    }


    public void CadastrarUsuario(Usuario usuario)
    {
        string sql = """
            INSERT INTO usuario (username, senha, nivel, avatar)
            VALUES (@username, @senha, @nivel, @avatar);
            """;

        var parametros = new Dictionary<string, object>
        {
            { "@username", usuario.Username! },
            { "@senha", usuario.Senha! },
            { "@nivel", usuario.nivel! },
            { "@avatar", usuario.Avatar! },
        }; 
        _db.ExecutarComando(sql, parametros);
    }

    public Usuario ? BuscarUsuarioPorUsername(string username)
    {
        string sql = "SELECT * FROM usuario WHERE username = @username";

        var parametros = new Dictionary<string, object>
        {
            { "@nome", username },  
        };

        DataTable dt = _db.PreencherTabela(sql, parametros);

         if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            return new Usuario
            {
                Id = Convert.ToInt32(row["id"]),
                Username = row["username"].ToString(),
                Senha = row["senha"].ToString(),
                nivel = row["nivel"].ToString(),
                Avatar = row["avatar"].ToString()
            };
        }
        return null;
    }

    public string CriptografarSenha(string senha)
    {
        return BCrypt.HashPassword(senha);
    }

    public void AlterarNivelAcesso(int usuarioId, NivelAcesso nivel)
    {
        string sql = "UPDATE usuario SET nivel = @nivel WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@nivel", nivel },  
            { "@id", usuarioId },  
        };

        _db.ExecutarComando(sql, parametros);
    }   

    public void UploadAvatar(int usuarioId, byte[] imagem)
    {
        string sql = "UPDATE usuario SET avatar = @avatar WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@avatar", imagem },
            { "@id", usuarioId },
        };

        _db.ExecutarComando(sql, parametros);
    }

}
// namespace Services;

// using System.Data;
// using Business.Interfaces;
// using Entities;

// public class UsuarioService : IUsuarioService
// {
//     private readonly DataBaseConnection _db = new DataBaseConnection();

//     public Usuario ? Login(string username, string senha)
//     {
//         string sql = "SELECT * FROM usuario WHERE email = @email AND senha = @senha";

//         var parametros = new Dictionary<string, object>
//         {
//             { "email", username },
//             { "senha", senha }
//         };

//         DataTable dt = _db.PreencherTabela(sql, parametros);

//         if (dt.Rows.Count > 0)
//         {
//             DataRow row = dt.Rows[0];
//             return new Usuario
//             {
//                 Id = row.Field<int>("id"),
//                 Username = row.Field<string>("username"),

//             };
//         }

//         return null; // email encontrado ou senha incorreta
//     }


//     // public int CadastrarUsuario(string username, string senha, string nivel, string avatar)
//     // {
//     //     using (MySqlConnection connection = new MySqlConnection(_connectionString))
//     //     {
//     //         connection.Open();
//     //         string sql = """
//     //             INSERT INTO usuario (username, senha, nivel, avatar)
//     //             VALUES (@username, @senha, @nivel, @avatar);
//     //             SELECT LAST_INSERT_ID();
//     //             """;

//     //         using var cmd = new MySqlCommand(sql, connection);
//     //         cmd.Parameters.AddWithValue("@username", username);
//     //         cmd.Parameters.AddWithValue("@senha", BCrypt.Net.BCrypt.HashPassword(senha));
//     //         cmd.Parameters.AddWithValue("@nivel", nivel);
//     //         cmd.Parameters.AddWithValue("@avatar", (object?)avatar ?? DBNull.Value);

//     //         cmd.ExecuteNonQuery();
//     //         return Convert.ToInt32(cmd.ExecuteScalar());
//     //     }
//     // }

//     // public List<Usuario> ListarUsuarios()
//     // {
//     //     List<Usuario> usuarios = new List<Usuario>();
//     //     using (MySqlConnection connection = new MySqlConnection(_connectionString))
//     //     {
//     //         connection.Open();
//     //         string sql = """
//     //             SELECT * FROM usuario;
//     //             """;

//     //         using var cmd = new MySqlCommand(sql, connection);
//     //         using var reader = cmd.ExecuteReader();

//     //         while (reader.Read())
//     //         {
//     //             Usuario usuario = new Usuario
//     //             {
//     //                 Id = reader.GetInt32("id"),
//     //                 Username = reader.GetString("username"),
//     //                 Senha = reader.GetString("senha"),
//     //                 nivel = reader.GetString("nivel"),
//     //                 Avatar = reader.GetString("avatar")
//     //             };
//     //             usuarios.Add(usuario);
//     //         }
//     //     }
//     //     return usuarios;
//     // }

//     // public Usuario ? ListarUsuarioPorUsername(string username)
//     // {
//     //     using (MySqlConnection connection = new MySqlConnection(_connectionString))
//     //     {
//     //         connection.Open();

//     //         string sql = """
//     //             SELECT * FROM usuario WHERE username = @username;
//     //             """;

//     //         using var cmd = new MySqlCommand(sql, connection);
//     //         cmd.Parameters.AddWithValue("@username", username);

//     //         using var reader = cmd.ExecuteReader();

//     //         while(reader.Read())
//     //         {
//     //             Usuario usuario = new Usuario
//     //             {
//     //                 Id = reader.GetInt32("id"),
//     //                 Username = reader.GetString("username"),
//     //                 Senha = reader.GetString("senha"),
//     //                 nivel = reader.GetString("nivel"),
//     //                 Avatar = reader.GetString("avatar")
//     //             };
//     //         }
//     //         return null;
//     //     }
//     // }
    
//     // public void AtualizarUsuario(int id, string ? username, string ? avatar, string ? senha)
//     // {
//     //     using (MySqlConnection connection = new MySqlConnection(_connectionString))
//     //     {
//     //         connection.Open();

//     //         var sqlParts = new List<string> { "UPDATE usuario SET" };
//     //         var parameters = new List<MySqlParameter>();
//     //         var conditions = new List<string>();

//     //         if (username != null)
//     //         {
//     //             sqlParts.Add("username = @username");
//     //             parameters.Add(new MySqlParameter("@username", username));
//     //         }

//     //         if (avatar != null)
//     //         {
//     //             sqlParts.Add("avatar = @avatar");
//     //             parameters.Add(new MySqlParameter("@avatar", avatar));
//     //         }

//     //          if (senha != null)
//     //         {
//     //             sqlParts.Add("senha = @senha");
//     //             parameters.Add(new MySqlParameter("@senha",
//     //             BCrypt.Net.BCrypt.HashPassword(senha)));
//     //         }

//     //         // nenhum campo, eu saio
//     //         if (parameters.Count == 0)
//     //         {
//     //             Console.WriteLine("Nenhum campo para atualizar.");
//     //             return;
//     //         }

//     //         string sql = string.Join(", ", sqlParts + " WHERE id = @id");
//     //         parameters.Add(new MySqlParameter("@id", id));

//     //         using var cmd = new MySqlCommand(sql, connection);
//     //         cmd.Parameters.AddRange(parameters.ToArray());

//     //         cmd.ExecuteNonQuery();
//     //     }
//     // }

//     // public void DeletarUsuario(int id)
//     // {
//     //     using var connection = new MySqlConnection(_connectionString);
//     //     connection.Open();

//     //     string sql = "DELETE FROM usuario WHERE id = @id";
//     //     using var cmd = new MySqlCommand(sql, connection);

//     //     cmd.Parameters.AddWithValue("@id", id);
//     //     cmd.ExecuteNonQuery();
//     // }

// }
namespace Data.Repositories;

using Entities;

public interface IUsuarioRepository
{
    Usuario? Login(string username, string senha);
    void CadastrarUsuario(Usuario usuario);
    Usuario? BuscarUsuarioPorUsername(string username);
    void AlterarNivelAcesso(int usuarioId, string nivel);
    void UploadAvatar(int usuarioId, byte[] imagem);
}

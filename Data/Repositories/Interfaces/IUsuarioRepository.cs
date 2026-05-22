namespace Data.Repositories.Interfaces;

using Entities;

public interface IUsuarioRepository
{
    Usuario? Login(string username, string senha);
    void CadastrarUsuario(Usuario usuario);
    Usuario? BuscarUsuarioPorUsername(string username);
    void AlterarNivelAcesso(int usuarioId, NivelAcesso nivel);
    void UploadAvatar(int usuarioId, string caminho_imagem);
    void DeletarUsuario(int usuarioId);
    void AtualizarUsuario(int id, string username);
    List<Usuario> ListarUsuarios();
}

using Entities;

namespace Business.Interfaces;

public interface IUsuarioService
{
    void CadastrarUsuario(Usuario usuario);
    Usuario? Login(string email, string senha);
    string CriptografarSenha(string senha);
    void AlterarNivelAcesso(int usuarioId, NivelAcesso nivel);
    void UploadAvatar(int usuarioId, string caminho_imagem);
    void DeletarUsuario(int usuarioId);
    void AtualizarUsuario(int id, string username);
    List<Usuario> ListarUsuarios();
}
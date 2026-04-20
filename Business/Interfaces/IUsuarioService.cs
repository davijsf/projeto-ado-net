using Entities;

namespace Business.Interfaces;

public interface IUsuarioService
{
    void CadastrarUsuario(Usuario usuario);
    Usuario? Login(string email, string senha);
    string CriptografarSenha(string senha);
    void AlterarNivelAcesso(int usuarioId, NivelAcesso nivel);
    void UploadAvatar(int usuarioId, byte[] imagem);
}
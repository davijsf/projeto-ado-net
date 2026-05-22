namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;
using BCrypt.Net;

public class UsuarioService : IUsuarioService
{
    private readonly UsuarioRepository _repository = new UsuarioRepository();

    public Usuario? Login(string username, string senha)
    {
        return _repository.Login(username, senha);
    }

    public void CadastrarUsuario(Usuario usuario)
    {
        _repository.CadastrarUsuario(usuario);
    }

    public Usuario? BuscarUsuarioPorUsername(string username)
    {
        return _repository.BuscarUsuarioPorUsername(username);
    }

    public string CriptografarSenha(string senha)
    {
        return BCrypt.HashPassword(senha);
    }

    public void AlterarNivelAcesso(int usuarioId, NivelAcesso nivel)
    {
        _repository.AlterarNivelAcesso(usuarioId, nivel);
    }

    public List<Usuario> ListarUsuarios()
    {
        return _repository.ListarUsuarios();
    }

    public void AtualizarUsuario(int id, string username)
    {
        _repository.AtualizarUsuario(id, username);
    }

    public void UploadAvatar(int usuarioId, string caminho_imagem)
    {
        _repository.UploadAvatar(usuarioId, caminho_imagem);
    }
    public void DeletarUsuario(int usuarioId)
    {
        _repository.DeletarUsuario(usuarioId);
    }
}

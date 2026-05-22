namespace ConsoleApp.Menus;

using Entities;
using Services;

public class UsuarioMenu
{
    private readonly UsuarioService _usuarioService;

    public UsuarioMenu(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public void ExibirMenu(Usuario usuarioLogado)
    {
        bool loop = true;
        while (loop)
        {
            if (usuarioLogado == null) 
                loop = MenuPrivado(usuarioLogado!);
        }
    }

    private bool MenuPrivado(Usuario usuario)
    {
        Console.WriteLine("=== MENU USUÁRIO ===");
        Console.WriteLine($"Usuário: {usuario.Username}\n");

        Console.WriteLine("1. Meu perfil");
        Console.WriteLine("2. Atualizar usuário");
        Console.WriteLine("3. Alterar nível (Admin)");
        Console.WriteLine("4. Deletar usuário (Admin)");
        Console.WriteLine("5. Buscar usuário");
        Console.WriteLine("6. Upload avatar");
        Console.WriteLine("7. Logout");
        Console.WriteLine("0. Sair");

        Console.Write("\nDigite: ");
        string opcao = Console.ReadLine() ?? "";

        switch (opcao)
        {
            case "1":
                ExibirPerfil(usuario);
                break;

            case "2":
                AtualizarUsuario(usuario);
                break;

            case "3":
                if (usuario.nivel == NivelAcesso.Admin)
                    AlterarNivelAcesso();
                else
                    AcessoNegado();
                break;

            case "4":
                if (usuario.nivel == NivelAcesso.Admin)
                    DeletarUsuario(usuario);
                else
                    AcessoNegado();
                break;

            case "5":
                BuscarUsuario();
                break;

            case "6":
                UploadAvatar(usuario);
                break;

            case "7":
                return false;

            case "0":
                Environment.Exit(0);
                break;
        }

        return true;
    }

    public Usuario? Login()
    {
        Console.Clear();
        Console.WriteLine("=== LOGIN ===");

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Senha: ");
        string senha = LerSenha();

        var usuario = _usuarioService.Login(username, senha);

        if (usuario == null)
        {
            Console.WriteLine("Login inválido!");
            Console.ReadKey();
        }

        return usuario;
    }

    private void AtualizarUsuario(Usuario usuario)
    {
        Console.Clear();

        Console.Write("Novo username: ");
        string novoUsername = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(novoUsername))
        {
            usuario.Username = novoUsername;
            _usuarioService.AtualizarUsuario(usuario.Id, novoUsername);
        }

        Console.WriteLine("Atualizado!");
        Console.ReadKey();
    }

    private void BuscarUsuario()
    {
        Console.Clear();

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";

        var usuario = _usuarioService.BuscarUsuarioPorUsername(username);

        if (usuario == null)
        {
            Console.WriteLine("Não encontrado!");
        }
        else
        {
            ExibirPerfil(usuario);
            return;
        }

        Console.ReadKey();
    }

    private void AlterarNivelAcesso()
    {
        Console.Clear();

        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.Write("Novo nível (0/1): ");
        NivelAcesso nivel = (NivelAcesso)int.Parse(Console.ReadLine()!);

        _usuarioService.AlterarNivelAcesso(id, nivel);

        Console.WriteLine("Alterado!");
        Console.ReadKey();
    }

    private void UploadAvatar(Usuario usuario)
    {
        Console.Clear();

        Console.Write("Caminho: ");
        string caminho = Console.ReadLine() ?? "";

        string? avatar = AvatarService.SalvarAvatar(usuario.Id, caminho);

        if (avatar != null)
        {
            usuario.Avatar = avatar;
            _usuarioService.UploadAvatar(usuario.Id, avatar);
        }

        Console.WriteLine("Avatar atualizado!");
        Console.ReadKey();
    }

    private void DeletarUsuario(Usuario usuario)
    {
        Console.Clear();

        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine()!);

        if (usuario.Id == id)
        {
            Console.WriteLine("Você não pode deletar sua própria conta.");    
            return;
        }

        _usuarioService.DeletarUsuario(id);

        Console.WriteLine("Deletado!");
        Console.ReadKey();
    }

    public void ExibirPerfil(Usuario usuario)
    {
        Console.Clear();

        Console.WriteLine($"ID: {usuario.Id}");
        Console.WriteLine($"Username: {usuario.Username}");
        Console.WriteLine($"Nível: {usuario.nivel}");
        Console.WriteLine($"Avatar: {usuario.Avatar}");

        Console.ReadKey();
    }

    private void AcessoNegado()
    {
        Console.WriteLine("Acesso negado!");
        Console.ReadKey();
    }

    private string LerSenha()
    {
        string senha = "";

        while (true)
        {
            var tecla = Console.ReadKey(true);

            if (tecla.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return senha;
            }

            if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
            {
                senha = senha[..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(tecla.KeyChar))
            {
                senha += tecla.KeyChar;
                Console.Write("*");
            }
        }
    }
}
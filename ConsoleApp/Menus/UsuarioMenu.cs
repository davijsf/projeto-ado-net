namespace ConsoleApp.Menus;

using Entities;
using Org.BouncyCastle.Tls;
using Services;

public class UsuarioMenu
{
    private readonly UsuarioService _usuarioService;

    public UsuarioMenu(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public void ExibirMenu()
    {
        bool loop = true;

        while(loop)
        {
            Console.Clear();
            Console.WriteLine("=== USUÁRIO MENU ===");
            Console.WriteLine("1. Cadastrar usuário");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Alterar Nível de acesso");
            Console.WriteLine("4. Upload em avatar");
            Console.WriteLine("0. Voltar");
            
            Console.Write("Digite: ");
            string opcao = Console.ReadLine()!;

            switch(opcao)
            {
                case "1": CadastrarUsuario(); break;
                case "2": Login(); break;
                case "3": AlterarNivelAcesso(); break;
                case "4": UploadAvatar(); break;
                case "0": loop = false; break;
                default: 
                    Console.WriteLine("Opção inválida."); 
                    Console.ReadKey();    
                    break;
            }
        }
    }

    public void CadastrarUsuario()
    {  
        Console.Clear();
        Console.WriteLine("=== CADASTRAR USUÁRIO ===");

        Console.Write("Username: ");
        string username = Console.ReadLine()!;

        Console.Write("Senha: ");
        string senha = Console.ReadLine()!;
        senha = _usuarioService.CriptografarSenha(senha);

        Console.Write("Nível: ");
        string nivel = Console.ReadLine()!;

        Console.Write("Avatar: ");
        string avatar = Console.ReadLine()!;

        // Vefirica se já existe um usuário com o mesmo username
        Usuario ? usuario = _usuarioService.BuscarUsuarioPorUsername(username);

        // Se não, cadastraaa
        if (usuario == null)
        {
            Console.WriteLine($"\nUsuário '{username}' não encontrado. Vamos cadastrá-lo!");

            usuario = new Usuario
            {
                Username = username,
                Senha = senha,
                nivel = nivel,
                Avatar = avatar
            };

            _usuarioService.CadastrarUsuario(usuario);
            Console.WriteLine($"Usuário '{usuario.Username}' cadastrado com sucesso!");
            Console.ReadKey();
        }
    }

    public void Login()
    {
        
    }

    public void CriptografarSenha()
    {
        
    }

    public void AlterarNivelAcesso()
    {
        
    }

    public void UploadAvatar()
    {
        
    }
}


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
            Console.WriteLine("5. Buscar usuário");
            Console.WriteLine("0. Voltar");
            
            Console.Write("Digite: ");
            string opcao = Console.ReadLine()!;

            switch(opcao)
            {
                case "1": CadastrarUsuario(); break;
                case "2": Login(); break;
                case "3": AlterarNivelAcesso(); break;
                case "4": UploadAvatar(); break;
                case "5": BuscarUsuario(); break;
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
        Console.Clear();
        Console.WriteLine("=== LOGIN ===");

        Console.Write("Username: ");
        string username = Console.ReadLine()!;

        Console.Write("Senha: ");
        string senha = Console.ReadLine()!;

        var login = _usuarioService.Login(username, senha);


        // Senha incorreta ou username inválido
        if (login == null)
        {
            Console.WriteLine("Username ou senha inválidos!");
            Console.ReadKey();
        }

        Console.WriteLine($"Bem vindo, {login?.Username}!");
        Console.ReadKey();

    }

    public void AlterarNivelAcesso()
    {
        Console.Clear();
        Console.WriteLine("Olá, ADM!");

        Console.Write("Id do usuário: ");
        int Id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Nivel do acesso: ");
        Console.WriteLine("0 - Comum");
        Console.WriteLine("1 - Admin");
        Console.Write("Escolha: ");

        NivelAcesso nivel;
        while (!Enum.TryParse(Console.ReadLine(), out nivel))
            Console.Write("Opção inválida. Escolha 0 (Comum) ou 1 (Admin): ");

        _usuarioService.AlterarNivelAcesso(Id, nivel);
        Console.ReadKey();
    }

    public void UploadAvatar()
    {
        
    }
     

    public void BuscarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCA DE USUÁRIO ===");

        Console.WriteLine("Username: ");
        string username = Console.ReadLine()!;

        var usuario = _usuarioService.BuscarUsuarioPorUsername(username);

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n--- Dados do Usuário ---");
        Console.WriteLine($"Id:       {usuario.Id}");
        Console.WriteLine($"Username: {usuario.Username}");
        Console.WriteLine($"Nível:    {usuario.nivel}");
        Console.WriteLine($"Avatar:   {usuario.Avatar ?? "Sem avatar"}");
        Console.WriteLine("------------------------");
        Console.ReadKey();

    }
}


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

    public void ExibirMenu()
    {
        bool loop = true;
        Usuario? usuarioLogado = null;

        while (loop)
        {
            Console.Clear();
            Console.WriteLine("=== USUÁRIO MENU ===");

            if (usuarioLogado == null)
            {
                Console.WriteLine("1. Cadastrar usuário");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Voltar");

                Console.Write("Digite: ");
                string opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "1": CadastrarUsuario(); break;
                    case "2": usuarioLogado = Login(); break;
                    case "0": loop = false; break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Logado como: {usuarioLogado.Username} [{usuarioLogado.nivel}]");

                if (usuarioLogado.nivel == NivelAcesso.Admin)
                    Console.WriteLine("3. Alterar Nível de acesso");
                    Console.WriteLine("4. Deletar usuário");
                    Console.WriteLine("5. Buscar usuário");

                Console.WriteLine("6. Upload em avatar");
                Console.WriteLine("7. Logout");
                Console.WriteLine("0. Voltar");

                Console.Write("Digite: ");
                string opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "3":
                        if (usuarioLogado.nivel == NivelAcesso.Admin)
                            AlterarNivelAcesso();
                        else
                        {
                            Console.WriteLine("Acesso negado!");
                            Console.ReadKey();
                        }
                        break;
                    case "4":
                        if (usuarioLogado.nivel == NivelAcesso.Admin)
                            DeletarUsuario();
                        else
                        {
                            Console.WriteLine("Acesso negado!");
                            Console.ReadKey();
                        }
                        break;    
                    case "5": BuscarUsuario(); break;
                    case "6": UploadAvatar(usuarioLogado); break;
                    case "7": usuarioLogado = null; break;
                    case "0": loop = false; break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
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

        NivelAcesso nivel;
        Console.Write("Nível (Comum ou Admin): ");
        while (!Enum.TryParse<NivelAcesso>(Console.ReadLine(), ignoreCase: true, out nivel))
            Console.Write("Nível inválido. Digite Comum ou Admin: ");

        Console.Write("Avatar: ");
        string avatar = Console.ReadLine()!;

        Usuario? usuario = _usuarioService.BuscarUsuarioPorUsername(username);

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
        else
        {
            Console.WriteLine($"Usuário '{username}' já existe!");
            Console.ReadKey();
        }
    }

    public Usuario? Login()
    {
        Console.Clear();
        Console.WriteLine("=== LOGIN ===");

        Console.Write("Username: ");
        string username = Console.ReadLine()!;

        Console.Write("Senha: ");
        string senha = LerSenha();

        var login = _usuarioService.Login(username, senha);

        if (login == null)
        {
            Console.WriteLine("Username ou senha inválidos!");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine($"Bem vindo, {login.Username}!");
        Console.ReadKey();
        return login;
    }

    private string LerSenha()
    {
        string senha = "";

        while (true)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(intercept: true); // intercept: true = não mostra no console

            if (tecla.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (tecla.Key == ConsoleKey.Backspace)
            {
                if (senha.Length > 0)
                {
                    senha = senha[..^1];
                    Console.Write("\b \b"); 
                }
            }
            else
            {
                senha += tecla.KeyChar;
                Console.Write("*"); 
            }
        }

        return senha;
    }
    public void AlterarNivelAcesso()
    {
        Console.Clear();
        Console.WriteLine("=== ALTERAR NÍVEL DE ACESSO ===");

        Console.Write("Id do usuário: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Nível de acesso:");
        Console.WriteLine("0 - Comum");
        Console.WriteLine("1 - Admin");
        Console.Write("Escolha: ");

        NivelAcesso nivel;
        while (!Enum.TryParse(Console.ReadLine(), out nivel))
            Console.Write("Opção inválida. Escolha 0 (Comum) ou 1 (Admin): ");

        _usuarioService.AlterarNivelAcesso(id, nivel);
        Console.WriteLine("Nível alterado com sucesso!");
        Console.ReadKey();
    }

    public void UploadAvatar(Usuario usuario)
    {
        Console.Clear();
        Console.WriteLine("=== UPLOAD AVATAR ===");

        Console.Write("Caminho da imagem: ");
        string caminhoOrigem = Console.ReadLine()!;

        string? nomeAvatar = AvatarService.SalvarAvatar(usuario.Id, caminhoOrigem);

        if (nomeAvatar != null)
        {
            _usuarioService.UploadAvatar(usuario.Id, nomeAvatar);
            usuario.Avatar = nomeAvatar; // Atualiza o objeto em memória também
            Console.WriteLine("Avatar atualizado com sucesso!");
        }

        Console.ReadKey();
    }

    public void BuscarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCA DE USUÁRIO ===");

        Console.Write("Username: ");
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
    public void DeletarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== DELETAR USUÁRIO ===");

        Console.Write("Id do usuário: ");
        int id = Convert.ToInt32(Console.ReadLine());

        _usuarioService.DeletarUsuario(id);
        Console.WriteLine("Usuário deletado com sucesso!");
        Console.ReadKey();
    }
    public void ExibirPerfil(Usuario usuario)
{
    Console.Clear();
    Console.WriteLine("=== MEU PERFIL ===\n");
    Console.WriteLine($"Id:       {usuario.Id}");
    Console.WriteLine($"Username: {usuario.Username}");
    Console.WriteLine($"Nível:    {usuario.nivel}");
    Console.WriteLine($"Avatar:   {usuario.Avatar ?? "Sem avatar"}");
    Console.ReadKey();
    }
}
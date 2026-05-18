namespace ConsoleApp.Menus;

using Entities;
using Services;

public class ClienteMenu
{
    private readonly ClienteService _clienteService;
    private readonly UsuarioService _usuarioService;

    public ClienteMenu(ClienteService clienteService, UsuarioService usuarioService)
    {
        _clienteService = clienteService;
        _usuarioService = usuarioService;
    }

    public void ExibirMenu()
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine("=== CLIENTES ===");
            Console.WriteLine("1. Cadastrar Cliente");
            Console.WriteLine("2. Listar Clientes");
            Console.WriteLine("3. Buscar por CPF");
            Console.WriteLine("4. Atualizar Cliente");
            Console.WriteLine("5. Remover Cliente");
            Console.WriteLine("0. Voltar");
            Console.Write("\nEscolha uma opção: ");

            switch (Console.ReadLine())
            {
                case "1": Cadastrar(); break;
                case "2": Listar(); break;
                case "3": BuscarPorCpf(); break;
                case "4": Atualizar(); break;
                case "5": Remover(); break;
                case "0": loop = false; break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

private void Cadastrar()
{
    Console.Clear();
    Console.WriteLine("=== CADASTRAR CLIENTE ===\n");

    Console.Write("Nome: ");
    string nome = Console.ReadLine()!;

    Console.Write("CPF: ");
    string cpf = Console.ReadLine()!;

    Console.Write("Email: ");
    string email = Console.ReadLine()!;

    Console.Write("\nJá possui usuário cadastrado? (s/n): ");
    string temUsuario = Console.ReadLine()!.ToLower();

    Usuario? usuario = null;

    if (temUsuario == "s")
    {
        // Vincula a um usuário já existente
        Console.Write("Username: ");
        string username = Console.ReadLine()!;

        usuario = _usuarioService.BuscarUsuarioPorUsername(username);

        if (usuario == null)
        {
            Console.WriteLine($"\nUsuário '{username}' não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Usuário encontrado: {usuario.Username} [{usuario.nivel}]");
    }
    else
    {
        // Cria um novo usuário
        Console.Write("Username: ");
        string username = Console.ReadLine()!;

        Console.Write("Senha: ");
        string senha = Console.ReadLine()!;

        var usuarioExistente = _usuarioService.BuscarUsuarioPorUsername(username);
        if (usuarioExistente != null)
        {
            Console.WriteLine($"\nUsername '{username}' já está em uso!");
            Console.ReadKey();
            return;
        }

        _usuarioService.CadastrarUsuario(new Usuario
        {
            Username = username,
            Senha    = _usuarioService.CriptografarSenha(senha),
            nivel    = NivelAcesso.Comum,
            Avatar   = ""
        });

        usuario = _usuarioService.BuscarUsuarioPorUsername(username);
    }

    try
    {
        _clienteService.CadastrarCliente(new Cliente
        {
            Nome      = nome,
            Cpf       = cpf,
            Email     = email,
            IdUsuario = usuario!.Id
        });

        Console.WriteLine("\nCliente cadastrado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nErro: {ex.Message}");
    }

    Console.ReadKey();
}

    private void Listar()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE CLIENTES ===\n");

        List<Cliente> clientes = _clienteService.ListarClientes();

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
            Console.ReadKey();
            return;
        }

        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Id:    {cliente.IdClient}");
            Console.WriteLine($"Nome:  {cliente.Nome}");
            Console.WriteLine($"CPF:   {cliente.Cpf}");
            Console.WriteLine($"Email: {cliente.Email}");
            Console.WriteLine(new string('-', 40));
        }

        Console.ReadKey();
    }

    private void BuscarPorCpf()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR POR CPF ===\n");

        Console.Write("CPF: ");
        string cpf = Console.ReadLine()!;

        var cliente = _clienteService.BuscarPorCpf(cpf);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nId:    {cliente.IdClient}");
        Console.WriteLine($"Nome:  {cliente.Nome}");
        Console.WriteLine($"CPF:   {cliente.Cpf}");
        Console.WriteLine($"Email: {cliente.Email}");
        Console.ReadKey();
    }

    private void Atualizar()
    {
        Console.Clear();
        Console.WriteLine("=== ATUALIZAR CLIENTE ===\n");

        Listar();

        Console.Write("Id do Cliente a atualizar: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Novo Nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Novo CPF: ");
        string cpf = Console.ReadLine()!;

        Console.Write("Novo Email: ");
        string email = Console.ReadLine()!;

        try
        {
            _clienteService.AtualizarCliente(new Cliente
            {
                IdClient = id,
                Nome     = nome,
                Cpf      = cpf,
                Email    = email
            });

            Console.WriteLine("\nCliente atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro: {ex.Message}");
        }

        Console.ReadKey();
    }

    private void Remover()
    {
        Console.Clear();
        Console.WriteLine("=== REMOVER CLIENTE ===\n");

        Listar();

        Console.Write("Id do Cliente a remover: ");
        int id = Convert.ToInt32(Console.ReadLine());

        _clienteService.RemoverCliente(id);

        Console.WriteLine("\nCliente removido com sucesso!");
        Console.ReadKey();
    }
}
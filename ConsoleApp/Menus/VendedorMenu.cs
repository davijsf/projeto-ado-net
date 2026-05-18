namespace ConsoleApp.Menus;

using Entities;
using Services;

public class VendedorMenu
{
    private readonly VendedorService _vendedorService;
    private readonly UsuarioService _usuarioService;

    public VendedorMenu(VendedorService vendedorService, UsuarioService usuarioService)
    {
        _vendedorService = vendedorService;
        _usuarioService  = usuarioService;
    }

    public void ExibirMenu()
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine("=== VENDEDORES ===");
            Console.WriteLine("1. Cadastrar Vendedor");
            Console.WriteLine("2. Listar Vendedores");
            Console.WriteLine("3. Atualizar Vendedor");
            Console.WriteLine("4. Remover Vendedor");
            Console.WriteLine("0. Voltar");
            Console.Write("\nEscolha uma opção: ");

            switch (Console.ReadLine())
            {
                case "1": Cadastrar(); break;
                case "2": Listar(); break;
                case "3": Atualizar(); break;
                case "4": Remover(); break;
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
    Console.WriteLine("=== CADASTRAR VENDEDOR ===\n");

    Console.Write("Nome: ");
    string nome = Console.ReadLine()!;

    Console.Write("Matrícula: ");
    string matricula = Console.ReadLine()!;

    Console.Write("Salário: ");
    double salario = Convert.ToDouble(Console.ReadLine());

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
            nivel    = NivelAcesso.Admin, // vendedor é admin
            Avatar   = ""
        });

        usuario = _usuarioService.BuscarUsuarioPorUsername(username);
    }

    _vendedorService.CadastrarVendedor(new Vendedor
    {
        Nome      = nome,
        Matricula = matricula,
        Salario   = salario,
        IdUsuario = usuario!.Id
    });

    Console.WriteLine("\nVendedor cadastrado com sucesso!");
    Console.ReadKey();
}

    private void Listar()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE VENDEDORES ===\n");

        List<Vendedor> vendedores = _vendedorService.ListarVendedores();

        if (vendedores.Count == 0)
        {
            Console.WriteLine("Nenhum vendedor cadastrado.");
            Console.ReadKey();
            return;
        }

        foreach (var vendedor in vendedores)
        {
            Console.WriteLine($"Id:        {vendedor.Id}");
            Console.WriteLine($"Nome:      {vendedor.Nome}");
            Console.WriteLine($"Matrícula: {vendedor.Matricula}");
            Console.WriteLine($"Salário:   R$ {vendedor.Salario:F2}");
            Console.WriteLine($"Id Usuário:{vendedor.IdUsuario}");
            Console.WriteLine(new string('-', 40));
        }

        Console.ReadKey();
    }

    private void Atualizar()
    {
        Console.Clear();
        Console.WriteLine("=== ATUALIZAR VENDEDOR ===\n");

        Listar();

        Console.Write("Id do Vendedor a atualizar: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Novo Nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Nova Matrícula: ");
        string matricula = Console.ReadLine()!;

        Console.Write("Novo Salário: ");
        double salario = Convert.ToDouble(Console.ReadLine());

        _vendedorService.AtualizarVendedor(new Vendedor
        {
            IdVend    = id,
            Nome      = nome,
            Matricula = matricula,
            Salario   = salario
        });

        Console.WriteLine("\nVendedor atualizado com sucesso!");
        Console.ReadKey();
    }

    private void Remover()
    {
        Console.Clear();
        Console.WriteLine("=== REMOVER VENDEDOR ===\n");

        Listar();

        Console.Write("Id do Vendedor a remover: ");
        int id = Convert.ToInt32(Console.ReadLine());

        _vendedorService.RemoverVendedor(id);

        Console.WriteLine("\nVendedor removido com sucesso!");
        Console.ReadKey();
    }
}
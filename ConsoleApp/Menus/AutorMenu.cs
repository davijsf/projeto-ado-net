namespace ConsoleApp.Menus;

using Entities;
using Services;

public class AutorMenu
{
    private readonly AutorService _autorService;

    public AutorMenu(AutorService autorService)
    {
        _autorService = autorService;
    }

    public void ExibirMenu()
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine("=== Autores ===");
            Console.WriteLine("1. Cadastrar Autor");
            Console.WriteLine("2. Listar Autores");
            Console.WriteLine("3. Atualizar Autor");
            Console.WriteLine("4. Remover Autor");
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
        Console.WriteLine("=== Cadastrar autor ===\n");

        Console.Write("Nome: ");
        string nome = Console.ReadLine()!;

        // Verifica se já existe
        var autorExistente = _autorService.BuscarPorNome(nome);
        if (autorExistente != null)
        {
            Console.WriteLine($"\nAutor '{nome}' já está cadastrado!");
            Console.ReadKey();
            return;
        }

        Console.Write("Nacionalidade: ");
        string nacionalidade = Console.ReadLine()!;

        _autorService.CadastrarAutor(new Autor
        {
            Nome          = nome,
            Nacionalidade = nacionalidade
        });

        Console.WriteLine("\nAutor cadastrado com sucesso!");
        Console.ReadKey();
    }

    private void Listar()
    {
        Console.Clear();
        Console.WriteLine("=== Lista de autores ===\n");

        List<Autor> autores = _autorService.ListarAutores();

        if (autores.Count == 0)
        {
            Console.WriteLine("Nenhum autor cadastrado.");
            Console.ReadKey();
            return;
        }

        foreach (var autor in autores)
        {
            Console.WriteLine($"Id:            {autor.Id}");
            Console.WriteLine($"Nome:          {autor.Nome}");
            Console.WriteLine($"Nacionalidade: {autor.Nacionalidade}");
            Console.WriteLine(new string('-', 40));
        }

        Console.ReadKey();
    }

    private void Atualizar()
    {
        Console.Clear();
        Console.WriteLine("=== Atualizar autor ===\n");

        Listar();

        Console.Write("Id do Autor a atualizar: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Novo Nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Nova Nacionalidade: ");
        string nacionalidade = Console.ReadLine()!;

        _autorService.AtualizarAutor(new Autor
        {
            Id            = id,
            Nome          = nome,
            Nacionalidade = nacionalidade
        });

        Console.WriteLine("\nAutor atualizado com sucesso!");
        Console.ReadKey();
    }

    private void Remover()
    {
        Console.Clear();
        Console.WriteLine("=== Remover autor ===\n");

        Listar();

        Console.Write("Id do Autor a remover: ");
        int id = Convert.ToInt32(Console.ReadLine());

        _autorService.RemoverAutor(id);

        Console.WriteLine("\nAutor removido com sucesso!");
        Console.ReadKey();
    }
}
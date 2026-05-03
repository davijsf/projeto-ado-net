namespace ConsoleApp.Menus;

using Services;
using Entities;

public class LivroMenu
{
    private readonly LivroService _livroService;

    public LivroMenu(LivroService livroService)
    {
        _livroService = livroService;
    }

    public void ExibirMenu()
    {
        bool voltar = false;

        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine("=== LIVROS ===");
            Console.WriteLine("1. Cadastrar Livro");
            Console.WriteLine("2. Listar Livros");
            Console.WriteLine("3. Consultar Livros por Autor");
            Console.WriteLine("4. Atualizar Livro");
            Console.WriteLine("5. Remover Livro");
            Console.WriteLine("0. Voltar");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1": CadastrarLivro(); break;
                case "2": ListarLivros(); break;
                case "3": ConsultarLivrosPorAutor(); break;
                case "4": AtualizarLivro(); break;
                case "5": RemoverLivro(); break;
                case "0": voltar = true; break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void CadastrarLivro()
    {
        Console.Clear();
        Console.WriteLine("=== CADASTRAR LIVRO ===");

        Console.Write("Título: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Preço: ");
        double preco = Convert.ToDouble(Console.ReadLine());

        Console.Write("Estoque: ");
        int estoque = Convert.ToInt32(Console.ReadLine());

        Console.Write("Id do Autor: ");
        int idAutor = Convert.ToInt32(Console.ReadLine());

        Livro livro = new Livro
        {
            Titulo  = titulo,
            Preco   = preco,
            Estoque = estoque,
            IdAutor = idAutor
        };

        _livroService.CadastrarLivro(livro);

        Console.WriteLine("\nLivro cadastrado com sucesso!");
        Console.ReadKey();
    }

    private void ListarLivros()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE LIVROS ===\n");

        List<Livro> livros = _livroService.ListarLivros();

        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado.");
        }
        else
        {
            foreach (Livro livro in livros)
            {
                Console.WriteLine($"Id: {livro.Id}");
                Console.WriteLine($"Título: {livro.Titulo}");
                Console.WriteLine($"Preço: R$ {livro.Preco:F2}");
                Console.WriteLine($"Estoque: {livro.Estoque}");
                Console.WriteLine($"Id Autor: {livro.IdAutor}");
                Console.WriteLine("----------------------------");
            }
        }

        Console.ReadKey();
    }

        private void ConsultarLivrosPorAutor()
    {
        Console.Clear();
        Console.WriteLine("=== CONSULTAR LIVROS POR AUTOR ===\n");

        Console.Write("Id do Autor: ");
        int autorId = Convert.ToInt32(Console.ReadLine());

        List<Livro> livros = _livroService.ConsultarLivrosPorAutor(autorId);

        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro encontrado para esse autor.");
        }
        else
        {
            foreach (Livro livro in livros)
            {
                Console.WriteLine($"Id: {livro.Id}");
                Console.WriteLine($"Título: {livro.Titulo}");
                Console.WriteLine($"Preço: R$ {livro.Preco:F2}");
                Console.WriteLine($"Estoque: {livro.Estoque}");
                Console.WriteLine("----------------------------");
            }
        }

        Console.ReadKey();
    }

    private void AtualizarLivro()
    {
        Console.Clear();
        Console.WriteLine("=== ATUALIZAR LIVRO ===\n");

        ListarLivros();

        Console.Write("Id do Livro a atualizar: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Novo Título: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Novo Preço: ");
        double preco = Convert.ToDouble(Console.ReadLine());

        Console.Write("Novo Estoque: ");
        int estoque = Convert.ToInt32(Console.ReadLine());

        Console.Write("Novo Id do Autor: ");
        int idAutor = Convert.ToInt32(Console.ReadLine());

        Livro livro = new Livro
        {
            Id      = id,
            Titulo  = titulo,
            Preco   = preco,
            Estoque = estoque,
            IdAutor = idAutor
        };

        _livroService.AtualizarLivro(livro);

        Console.WriteLine("\nLivro atualizado com sucesso!");
        Console.ReadKey();
    }

    private void RemoverLivro()
    {
        Console.Clear();
        Console.WriteLine("=== REMOVER LIVRO ===\n");

        ListarLivros();

        Console.Write("Id do Livro a remover: ");
        int id = Convert.ToInt32(Console.ReadLine());

        _livroService.RemoverLivro(id);

        Console.WriteLine("\nLivro removido com sucesso!");
        Console.ReadKey();
    }
}
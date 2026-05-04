// See https://aka.ms/new-console-template for more information
namespace ConsoleApp;

using Data;
using Services;
using ConsoleApp.Menus;

class Application {

    public static void Main(string[] args) {
        DataBaseConnection db = new DataBaseConnection();

        ClienteService clienteService = new ClienteService();
        //UsuarioService usuarioService = new UsuarioService();
        VendedorService vendedorService = new VendedorService();
        LivroService livroService = new LivroService();
        VendaService vendaService = new VendaService();
        AutorService autorService = new AutorService();

        
        LivroMenu livroMenu = new LivroMenu(livroService, autorService);
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("=== LIVRARIA ===");
            Console.WriteLine("1. Livros");
            Console.WriteLine("0. Sair");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1": livroMenu.ExibirMenu(); break;
                case "0": sair = true; break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
        

    }
}


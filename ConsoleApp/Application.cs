namespace ConsoleApp;

using ConsoleApp.Menus;
using Data;
using Entities;
using Services;

class Application
{
    public static void Main(string[] args)
    {
        UsuarioService usuarioService = new UsuarioService();
        LivroService livroService = new LivroService();
        AutorService autorService = new AutorService();
        VendaService vendaService = new VendaService();
        VendedorService vendedorService = new VendedorService();
        ClienteService clienteService = new ClienteService();

        LivroMenu livroMenu = new LivroMenu(livroService, autorService);
        UsuarioMenu usuarioMenu = new UsuarioMenu(usuarioService);
        VendaMenu vendaMenu = new VendaMenu(livroService, vendaService, clienteService);
        AutorMenu autorMenu = new AutorMenu(autorService);
        VendedorMenu vendedorMenu = new VendedorMenu(vendedorService, usuarioService);
        ClienteMenu clienteMenu = new ClienteMenu(clienteService, usuarioService);

        // Login antes de acessar o sistema
        Usuario? usuarioLogado = null;

        while (usuarioLogado == null)
        {
            Console.Clear();
            Console.WriteLine("=== LIVRARIA ===");
            Console.WriteLine("1. Login");
            Console.WriteLine("0. Sair");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    usuarioLogado = usuarioMenu.Login();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }

        // Menu principal baseado no nível de acesso
        bool sair = false;
        while (!sair)
        {
            Console.Clear();

            if (usuarioLogado.nivel == NivelAcesso.Admin)
            {
                Console.WriteLine($"=== LIVRARIA [ADMIN] - {usuarioLogado.Username} ===");
                Console.WriteLine("1. Livros");
                Console.WriteLine("2. Clientes");
                Console.WriteLine("3. Vendedores");
                Console.WriteLine("4. Autores");
                Console.WriteLine("5. Vendas");
                Console.WriteLine("6. Usuários");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": livroMenu.ExibirMenu(); break;
                    case "2": clienteMenu.ExibirMenu(); break;
                    case "3": vendedorMenu.ExibirMenu(); break;
                    case "4": autorMenu.ExibirMenu(); break;
                    case "5": vendaMenu.ExibirMenu(usuarioLogado); break;
                    case "6": usuarioMenu.ExibirMenu(); break;
                    case "0": sair = true; break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"=== LIVRARIA [COMUM] - {usuarioLogado.Username} ===");
                Console.WriteLine("1. Vendas");
                Console.WriteLine("2. Meu Perfil");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": vendaMenu.ExibirMenu(usuarioLogado); break;
                    case "2": usuarioMenu.ExibirPerfil(usuarioLogado); break;
                    case "0": sair = true; break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}


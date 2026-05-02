// See https://aka.ms/new-console-template for more information
namespace ConsoleApp;

using Data;
using Services;

class Application {

    public static void Main(string[] args) {
        DataBaseConnection db = new DataBaseConnection();

        ClienteService clienteService = new ClienteService();
        UsuarioService usuarioService = new UsuarioService();
        VendedorService vendedorService = new VendedorService();
        LivroService livroService = new LivroService();
        VendaService vendaService = new VendaService();
        AutorService autorService = new AutorService();

        

    }
}


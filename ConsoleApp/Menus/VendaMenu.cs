namespace ConsoleApp.Menus;
using Business.Interfaces;
using Entities;
using Services;
public class VendaMenu
{
    private readonly VendaService _vendaService;
    private readonly LivroService _livroService;
    private readonly ClienteService _clienteService;
    private Carrinho? _carrinho;

    public VendaMenu(LivroService livroService, VendaService vendaService, ClienteService clienteService)
    {
        _livroService = new LivroService();
        _vendaService = new VendaService();
        _clienteService = new ClienteService();
    }

    public void ExibirMenu(Usuario usuarioLogado)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Vendas ===");
            Console.WriteLine("1. Nova Venda");
            Console.WriteLine("2. Listar Vendas");

            if (usuarioLogado.nivel == NivelAcesso.Admin)
                Console.WriteLine("3. Consultar Vendas por Cliente");
            Console.WriteLine("0. Voltar");
            Console.Write("\nEscolha uma opção: ");
            string escolha = Console.ReadLine() ?? "";

            switch (escolha)
            {
                case "1": NovaVenda(usuarioLogado); break;
                case "2": ListarVendas(); break;
                case "3": 
                    if (usuarioLogado.nivel == NivelAcesso.Admin)
                        ConsultarVendasPorCliente(); 
                    else
                    {
                        Console.WriteLine("Acesso negado!");
                        Console.ReadKey();
                    }
                    break;
                case "0": loop = false; break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void NovaVenda(Usuario usuarioLogado)
    {
        Console.Clear();
        Console.WriteLine("=== Nova Venda ===");   

        int idCliente;
        int idVendedor;

        if (usuarioLogado.nivel == NivelAcesso.Admin)
        {
            Console.Write("ID do Cliente: ");
            idCliente = int.Parse(Console.ReadLine() !);

            Console.Write("ID do Vendedor: ");
            idVendedor = int.Parse(Console.ReadLine() !);
        }
        else
        {
            var cliente = _clienteService.BuscarPorIdUsuario(usuarioLogado.Id);

            if (cliente == null)
            {
                Console.WriteLine("Você não está cadastrado como cliente!");
                Console.WriteLine("Entre em contato com um administrador.");
                Console.ReadKey();
                return;
            }

            idCliente  = cliente.IdClient;
            idVendedor = 0; // sem vendedor para compra online
            Console.WriteLine($"Cliente: {cliente.Nome}");
        }

        _carrinho = new Carrinho
        {
            IdCliente = idCliente,
            IdVendedor = idVendedor
        };

        bool loop = true;
        while (loop)
        {
            Console.WriteLine("=== Carrinho ===");
            ExibirCarrinho();
            Console.WriteLine("\n1. Adicionar Livro");
            Console.WriteLine("2. Remover Livro");
            Console.WriteLine("3. Confirmar Venda");
            Console.WriteLine("0. Cancelar");
            Console.Write("\nEscolha uma opção: ");
            string escolha = Console.ReadLine() ?? "";

            switch (escolha)
            {
                case "1": AdicionarAoCarrinho(); break;
                case "2": RemoverDoCarrinho(); break;
                case "3": 
                    if (ConfirmarVenda()) 
                        loop = false;   
                    break;
                case "0":
                    Console.WriteLine("Venda cancelada!");
                    Console.ReadKey();
                    loop = false; 
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }

        _carrinho = null;
    }

    private void ExibirCarrinho()
    {
        if (_carrinho == null || _carrinho.Itens.Count == 0)
        {
            Console.WriteLine("Carrinho vazio.");
            return;
        }

        Console.WriteLine($"{"#", -4} {"Titulo", -30} {"Qtd", -5} {"Preço Unit.", -12} {"Subtotal"}");
        Console.WriteLine(new string('-', 60));

        for (int i = 0; i < _carrinho.Itens.Count; i++)
        {
            var item = _carrinho.Itens[i];
            Console.WriteLine($"{i + 1, -4} {item.TituloLivro, -30} {item.Quantidade, -5} R$ {item.PrecoUnitario, -10:F2} R$ {item.SubTotal:F2}");
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"{"Total:", -50} R$ {_carrinho.Total:F2}");
    }

    private void AdicionarAoCarrinho()
    {
        Console.Clear();
        Console.WriteLine("=== Adicionar livro ===\n");

        Console.Write("Nome do Livro: ");
        string nomeLivro = Console.ReadLine()!;

        var livro = _livroService.BuscarPorNome(nomeLivro);

        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Encontrado: {livro.Titulo} | R$ {livro.Preco:F2} | Estoque: {livro.Estoque}");
        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine()!);

        if (quantidade <= 0 || quantidade > livro.Estoque)
        {
            Console.WriteLine("Quantidade inválida ou estoque insuficiente!");
            Console.ReadKey();
            return;
        }

        // Verifica se o livro já está no carrinho
        var itemExistente = _carrinho!.Itens.FirstOrDefault(i => i.IdLivro == livro.Id);

        if (itemExistente != null)
        {
            itemExistente.Quantidade += quantidade;
        }
        else
        {
            _carrinho.Itens.Add(new ItemCarrinho
            {
                IdLivro       = livro.Id,
                TituloLivro   = livro.Titulo,
                Quantidade    = quantidade,
                PrecoUnitario = (decimal)livro.Preco
            });
        }

        Console.WriteLine($"\nAdicionado! Subtotal: R$ {quantidade * (decimal)livro.Preco:F2}");
        Console.ReadKey();
    }    

    private void RemoverDoCarrinho()
    {
        Console.Clear();
        Console.WriteLine("=== REMOVER LIVRO ===\n");

        ExibirCarrinho();

        if (_carrinho!.Itens.Count == 0)
        {
            Console.ReadKey();
            return;
        }

        Console.Write("\nNúmero do item a remover: ");
        int numero = int.Parse(Console.ReadLine()!);

        if (numero < 1 || numero > _carrinho.Itens.Count)
        {
            Console.WriteLine("Item inválido!");
            Console.ReadKey();
            return;
        }

        _carrinho.Itens.RemoveAt(numero - 1);
        Console.WriteLine("Item removido!");
        Console.ReadKey();
    }

    private bool ConfirmarVenda()
    {
        if (_carrinho!.Itens.Count == 0)
        {
            Console.WriteLine("\nCarrinho vazio! Adicione itens antes de confirmar.");
            Console.ReadKey();
            return false;
        }

        Console.Clear();
        Console.WriteLine("=== CONFIRMAR VENDA ===\n");
        ExibirCarrinho();

        Console.Write("\nConfirmar? (s/n): ");
        if (Console.ReadLine()!.ToLower() != "s")
            return false;

        // Cria a venda no banco
        var venda = new Venda
        {
            IdCliente  = _carrinho.IdCliente,
            IdVendedor = _carrinho.IdVendedor,
            DataVenda  = DateTime.Now,
            Total      = (double)_carrinho.Total
        };

        _vendaService.RegistrarVenda(venda);

        // Salva cada item no banco
        foreach (var item in _carrinho.Itens)
        {
            var itemVenda = new ItemVenda
            {
                IdLivro    = item.IdLivro,
                Quantidade = item.Quantidade,
                SubTotal   = item.SubTotal,
                IdVenda    = venda.Id
            };

            _vendaService.AdicionarItem(venda.Id, itemVenda);
        }

        Console.WriteLine($"\nVenda #{venda.Id} confirmada! Total: R$ {_carrinho.Total:F2}");
        Console.ReadKey();
        return true;
    }

    private void ListarVendas()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE VENDAS ===\n");

        List<Venda> vendas = _vendaService.ListarVendas();

        if (vendas.Count == 0)
        {
            Console.WriteLine("Nenhuma venda registrada.");
            Console.ReadKey();
            return;
        }

        foreach (var venda in vendas)
        {
            Console.WriteLine($"Id: {venda.Id}");
            Console.WriteLine($"Data: {venda.DataVenda:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Cliente: {venda.Cliente?.Nome}");
            Console.WriteLine($"Vendedor: {venda.Vendedor?.Nome}");
            Console.WriteLine($"Total: R$ {venda.Total:F2}");
            Console.WriteLine(new string('-', 40));
        }

        Console.ReadKey();
    }

    private void ConsultarVendasPorCliente()
    {
        Console.Clear();
        Console.WriteLine("=== VENDAS POR CLIENTE ===\n");

        Console.Write("Id do Cliente: ");
        int clienteId = int.Parse(Console.ReadLine()!);

        List<Venda> vendas = _vendaService.ConsultarVendasPorCliente(clienteId);

        if (vendas.Count == 0)
        {
            Console.WriteLine("Nenhuma venda encontrada para esse cliente.");
            Console.ReadKey();
            return;
        }

        foreach (var venda in vendas)
        {
            Console.WriteLine($"Id: {venda.Id}");
            Console.WriteLine($"Data: {venda.DataVenda:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Total: R$ {venda.Total:F2}");
            Console.WriteLine(new string('-', 40));
        }

        Console.ReadKey();
    }
}
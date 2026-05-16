namespace Menus;
using Business.Interfaces;
using Entities;
using Services;
public class VendaMenu
{
    private readonly VendaService _vendaService;
    private readonly LivroService _livroService;
    public VendaMenu()
    {
        _livroService = new LivroService();
        _vendaService = new VendaService();
    }

    public void ExibirMenu()
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Vendas ===");
            Console.WriteLine("1. Registrar Venda");
            Console.WriteLine("2. Adicionar Item à Venda");
            Console.WriteLine("3. Remover Item da Venda");
            Console.WriteLine("4. Calcular Total da Venda");
            Console.WriteLine("5. Listar Vendas");
            Console.WriteLine("6. Consultar Vendas por Cliente");
            Console.WriteLine("0. Voltar");
            Console.Write("Digite: ");
            string escolha = Console.ReadLine() ?? "";

            switch (escolha)
            {
                case "1": RegistrarVenda(); break;
                case "2": AdicionarItem(); break;
                case "3": RemoverItem(); break;
                case "4": CalcularTotal(); break;
                case "5": ListarVendas(); break;
                case "6": ConsultarVendasPorCliente(); break;
                case "0": loop = false; break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void RegistrarVenda()
    {
        Console.Write("Digite o ID do cliente: ");
        int clienteId = int.Parse(Console.ReadLine()!);

        var venda = new Venda
        {
            IdCliente = clienteId,
            DataVenda = DateTime.Now
        };

        _vendaService.RegistrarVenda(venda);
        Console.WriteLine($"Venda registrada com ID: {venda.Id}");
        Console.ReadKey();
    }

    private void AdicionarItem()
    {
       Console.WriteLine("id do livro: ");
         int livroId = int.Parse(Console.ReadLine()!);
         
        var livro = _livroService.ConsultarLivroPorId(livroId);
        if (livro == null)        {
            Console.WriteLine("Livro não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine()!);
        if (quantidade > livro.Estoque || quantidade <= 0)
        {
            Console.WriteLine("Estoque insuficiente ou valor inválido!");
            Console.ReadKey();
            return;
        }

        var item = new ItemVenda
        {
            IdLivro = livroId,
            Quantidade = quantidade,
            SubTotal = livro.Preco
        };

        _vendaService.AdicionarItem(livroId, item);
        Console.WriteLine("Item adicionado à venda!");
    }

    private void RemoverItem()
    {
        Console.WriteLine("ID da venda: ");
        int vendaId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("ID do item: ");
        int itemId = int.Parse(Console.ReadLine()!);

        _vendaService.RemoverItem(vendaId, itemId);
        Console.WriteLine("Item removido da venda!");
        // fazer o carrinho de compras, para mostrar os itens da venda e o id do item para remover
       
    }

    private void CalcularTotal()
    {
        
    }

    private void ListarVendas()
    {
    
    }

    private void ConsultarVendasPorCliente()
    {
        
    }
}
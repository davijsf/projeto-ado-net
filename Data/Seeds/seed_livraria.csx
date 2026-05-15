#!/usr/bin/env dotnet-script
// seed_livraria.csx
#r "nuget: MySqlConnector, 2.3.7"
#r "nuget: BCrypt.net-Next, 4.0.3"

using MySqlConnector;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Entre no dir \Seeds e rode:
// Código para rodar: dotnet script .\seed_livraria.csx

await Main();

async Task Main(){
    var connectionString = "server=localhost;database=livraria_ado_net;uid=root;pwd=1234";

    await using var conn = new MySqlConnection(connectionString);
    await conn.OpenAsync();
    Console.WriteLine("Conexão feita com sucesso!");

    await InserirUsuarios(conn);
    await InserirClientes(conn);
    await InserirVendedores(conn);
    await InserirAutores(conn);
    await InserirLivros(conn);
    await InserirVendas(conn);

    Console.WriteLine("\nBanco populado com sucesso!");

    // USUÁRIOS
    async Task InserirUsuarios(MySqlConnection c)
    {
        Console.WriteLine("Inserindo usuários ...");

        var usuarios = new[]
        {
            (username: "admin", senha: "admin123", nivel:"admin", avatar:"admin.png"),
            (username: "joao.vendas", senha: "joao123", nivel:"comum", avatar:"joao.png"),
            (username: "maria.vendas", senha: "maria123", nivel:"comum", avatar:"maria.png"),
            (username: "carlos.v", senha: "carlos123", nivel:"comum", avatar:"carlos.png"),
            (username: "ana.v", senha: "ana123", nivel:"comum", avatar:"ana.png")
        };

        const string sql = """
            INSERT IGNORE INTO usuario (username, senha, nivel, avatar) 
            VALUES (@username, @senha, @nivel, @avatar);
            """;

        foreach (var u in usuarios)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(u.senha);
            await using var cmd = new MySqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@username", u.username);
            cmd.Parameters.AddWithValue("@senha", hash);
            cmd.Parameters.AddWithValue("@nivel", u.nivel);
            cmd.Parameters.AddWithValue("@avatar", u.avatar);
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"    + {u.username} [{u.nivel}]");
        }
    }

    // CLIENTES

    async Task InserirClientes(MySqlConnection c)
    {
        Console.WriteLine("Inserindo clientes ...");

        var clientes = new[]
        {
            (nome: "Fernanda Oliveira",  cpf: "111.222.333-44", email: "fernanda@email.com"),
            (nome: "Ricardo Souza",      cpf: "222.333.444-55", email: "ricardo@email.com"),
            (nome: "Patrícia Lima",      cpf: "333.444.555-66", email: "patricia@email.com"),
            (nome: "Lucas Mendes",       cpf: "444.555.666-77", email: "lucas@email.com"),
            (nome: "Juliana Castro",     cpf: "555.666.777-88", email: "juliana@email.com"),
            (nome: "Marcos Pereira",     cpf: "666.777.888-99", email: "marcos@email.com"),
        };

        const string sql = """
            INSERT IGNORE INTO cliente(nome, cpf, email)
            VALUES (@nome, @cpf, @email)
        """;

        foreach (var cl in clientes)
        {
            await using var cmd = new MySqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@nome", cl.nome);
            cmd.Parameters.AddWithValue("@cpf", cl.cpf);
            cmd.Parameters.AddWithValue("@email", cl.email);
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"    + {cl.nome}");
        }
    }

    // VENDEDORES (vinculados aos usuários comuns)

    async Task InserirVendedores(MySqlConnection c)
    {
        Console.WriteLine("Inserindo vendedores ...");

        // busca IDs dos usuarios de nivel 'comun' na ordem de inserção
        var usuarioIds = new List<int>();
        await using (var q = new MySqlCommand(
            "SELECT id FROM usuario WHERE nivel = 'comum' ORDER BY id", c))
        await using (var r = await q.ExecuteReaderAsync())
            while (await r.ReadAsync())
                usuarioIds.Add(r.GetInt32(0));

        var vendedores = new[]
        {
            (nome: "João Silva",    matricula: "VND-001", salario: 2500.00m),
            (nome: "Maria Santos",  matricula: "VND-002", salario: 2800.00m),
            (nome: "Carlos Rocha",  matricula: "VND-003", salario: 2600.00m),
            (nome: "Ana Ferreira",  matricula: "VND-004", salario: 3000.00m),
        };

        const string sql = """
            INSERT IGNORE INTO vendedor (nome, matricula, salario, id_usuario)
            VALUES (@nome, @matricula, @salario, @idUsuario);
            """;

        for (int i = 0; i < vendedores.Length; i++)
        {
            var v = vendedores[i];
            int? idUsr = i < usuarioIds.Count ? usuarioIds[i] : null;

            await using var cmd = new MySqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@nome", v.nome);
            cmd.Parameters.AddWithValue("@matricula", v.matricula);
            cmd.Parameters.AddWithValue("@salario", v.salario);
            cmd.Parameters.AddWithValue("@idUsuario", (object)idUsr ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"    + {v.nome} (usuário #{idUsr})");
        }
    }

    // Autores

    async Task InserirAutores(MySqlConnection c)
    {
        Console.WriteLine("Inserindo autores ...");

        var autores = new[]
        {
            (nome: "Machado de Assis",    nacionalidade: "Brasileiro"),
            (nome: "Clarice Lispector",   nacionalidade: "Brasileira"),
            (nome: "George Orwell",       nacionalidade: "Britânico"),
            (nome: "Gabriel García Márquez", nacionalidade: "Colombiano"),
            (nome: "J.K. Rowling",        nacionalidade: "Britânica"),
            (nome: "Fiódor Dostoiévski",  nacionalidade: "Russo"),
        };

        string sql = """
            INSERT IGNORE INTO autor (nome, nacionalidade)
            VALUES (@nome, @nacionalidade);
            """;

        foreach(var a in autores)
        {
            await using var cmd = new MySqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@nome", a.nome);
            cmd.Parameters.AddWithValue("@nacionalidade", a.nacionalidade);
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"    + {a.nome}");
        }
    }

    async Task InserirLivros(MySqlConnection c)
    {
        Console.WriteLine("Inserindo livros ...");

        // mapeia o nome do autor -> id
        var autorIds = new Dictionary<string, int>();
        await using (var q = new MySqlCommand("SELECT id, nome FROM autor", c))
        await using (var r = await q.ExecuteReaderAsync())
            while (await r.ReadAsync())
                autorIds[r.GetString(1)] = r.GetInt32(0);

        var livros = new[]
        {
            (titulo: "Dom Casmurro",                  preco: 39.90m,  estoque: 25, autor: "Machado de Assis"),
            (titulo: "A Hora da Estrela",              preco: 34.90m,  estoque: 18, autor: "Clarice Lispector"),
            (titulo: "1984",                           preco: 44.90m,  estoque: 30, autor: "George Orwell"),
            (titulo: "A Revolução dos Bichos",         preco: 29.90m,  estoque: 40, autor: "George Orwell"),
            (titulo: "Cem Anos de Solidão",            preco: 54.90m,  estoque: 15, autor: "Gabriel García Márquez"),
            (titulo: "Harry Potter e a Pedra Filosofal",preco: 49.90m, estoque: 50, autor: "J.K. Rowling"),
            (titulo: "Crime e Castigo",                preco: 59.90m,  estoque: 12, autor: "Fiódor Dostoiévski"),
            (titulo: "Memórias Póstumas de Brás Cubas",preco: 36.90m,  estoque: 20, autor: "Machado de Assis"),
        };

        const string sql = """
            INSERT IGNORE INTO livro (titulo, preco, estoque, id_autor)
            VALUES (@titulo, @preco, @estoque, @idAutor);
            """;

        foreach (var l in livros)
        {
            autorIds.TryGetValue(l.autor, out int idAutor);
            await using var cmd = new MySqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@titulo", l.titulo);
            cmd.Parameters.AddWithValue("@preco", l.preco);
            cmd.Parameters.AddWithValue("@estoque", l.estoque);
            cmd.Parameters.AddWithValue("@idAutor", idAutor);
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"    + {l.titulo} - R$ {l.preco:F2}");
        }
    }

    // Vendas + itens

    async Task InserirVendas(MySqlConnection c)
    {
        Console.WriteLine("Inserindo vendas e itens ...");

        var clienteIds = await BuscarIds(c, "SELECT id FROM cliente");
        var vendedorIds = await BuscarIds(c, "SELECT id FROM vendedor");
        var livros = await BuscarLivros(c);

        if (clienteIds.Count == 0 || vendedorIds.Count == 0 || livros.Count == 0)
        {
            Console.WriteLine("Dados insuficientes para gerar vendas.");
            return;
        }

        // Pegar clientes, vendedores aleatórios
        var rng = new Random(42);

        // Gera 8 vendas com 1 a 3 itens cada
        for(int v = 0; v < 8; v++)
        {
            int idCliente = clienteIds[rng.Next(clienteIds.Count)];
            int idVendedor = vendedorIds[rng.Next(vendedorIds.Count)];
            var dataVenda = DateTime.Now.AddDays(-rng.Next(0, 60));

            // itens distintos para a venda
            var itens = new List<(int idLivro, decimal preco, int qtd)>();
            var indicesSorteados = new HashSet<int>();
            int numItens = rng.Next(1, 4);

            while (itens.Count < numItens)
            {
                int idx = rng.Next(livros.Count);
                if (!indicesSorteados.Add(idx)) continue;
                int qtd = rng.Next(1, 4);
                itens.Add((livros[idx].id, livros[idx].preco, qtd));
            }

            decimal totalVenda = 0;
            foreach (var i in itens) totalVenda += i.preco * i.qtd;

            // insere a venda
            int idVenda;
            const string sqlVenda = """
                INSERT INTO venda (data, total, id_cliente, id_vendedor)
                VALUES (@data, @total, @idCliente, @idVendedor);
                """;

            await using (var cmd = new MySqlCommand(sqlVenda, c))
            {
                cmd.Parameters.AddWithValue("@data", dataVenda);
                cmd.Parameters.AddWithValue("@total", totalVenda);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
                
                await cmd.ExecuteNonQueryAsync();
                idVenda = Convert.ToInt32(cmd.LastInsertedId);
            }

            // insere os itens
            const string sqlItem = """
                INSERT INTO itemvenda (quantidade, subtotal, id_livro, id_venda)
                VALUES (@qtd, @subtotal, @idLivro, @idVenda);
                """;

            foreach (var (idLivro, preco, qtd) in itens)
            {
                await using var cmdItem = new MySqlCommand(sqlItem, c);
                cmdItem.Parameters.AddWithValue("@qtd", qtd);
                cmdItem.Parameters.AddWithValue("@subtotal", preco * qtd);
                cmdItem.Parameters.AddWithValue("@idLivro", idLivro);
                cmdItem.Parameters.AddWithValue("@idVenda", idVenda);
                await cmdItem.ExecuteNonQueryAsync();
            }

            Console.WriteLine($"    + Venda #{idVenda} | cliente#{idCliente} | {itens.Count} item(ns) | R$ {totalVenda:F2}");
        }

    }

    // HELPERS (funções)

    async Task<List<int>> BuscarIds(MySqlConnection c, string sql)
    {
        var ids = new List<int>();
        await using var cmd = new MySqlCommand(sql, c);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) ids.Add(r.GetInt32(0));
        return ids;
    }

    async Task<List<(int id, decimal preco)>> BuscarLivros(MySqlConnection c)
    {
        var lista = new List<(int, decimal)>();
        await using var cmd = new MySqlCommand("SELECT id, preco FROM livro", c);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) lista.Add((r.GetInt32(0), r.GetDecimal(1)));
        return lista;
    }
}

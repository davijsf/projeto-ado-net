// See https://aka.ms/new-console-template for more information
namespace ConsoleApp;

using System.Data;
using Data;

class Application {

    public static void Main(string[] args) {

        DataBaseConnection db = new DataBaseConnection();


        // Teste:
         db.ExecutarComando(
        "INSERT INTO cliente (nome, cpf, email) VALUES (@NOME, @EMAIL, @CPF)",
            new Dictionary<string, object> {
                { "@NOME",  "Davi"},
                { "@EMAIL", "davi@email.com"},
                { "@CPF", "777.111.000-65"}
            }
        );

        DataTable clientes = db.PreencherTabela("SELECT * FROM clientes");
        Console.WriteLine($"Conexão OK! Total de clientes: {clientes.Rows.Count}");
    }
}


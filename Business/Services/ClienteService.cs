namespace Services;

using Data;
using System.Data;
using Entities;
using Business.Interfaces;

public class ClienteService : IClienteService
{
    private readonly DataBaseConnection _db = new DataBaseConnection();

    //refatorado com problemas 
    public void CadastrarCliente(Cliente cliente)
    {
        // Deixei sem id usuário, por enquanto
        string sql = "INSERT INTO cliente (nome, cpf, email) VALUES (@nome, @cpf, @email);";
        var parametros = new Dictionary<string, object>
        {
            { "@nome",      cliente.Nome! },
            { "@cpf", cliente.Cpf! },
            { "@email",   cliente.Email! }
        };

        _db.ExecutarComando(sql, parametros);
    }

    public List<Cliente> ListarClientes()
    {
        string sql = "SELECT * FROM cliente";
       
       DataTable dt = _db.PreencherTabela(sql);

       return dt.AsEnumerable()
        .Select(row => new Cliente
        {
            IdClient = row.Field<int>("id"),
            Id = row.Field<int>("id_usuario"),
            Nome = row.Field<string>("nome"),
            Cpf = row.Field<string>("cpf"),
            Email = row.Field<string>("email"),
        })
        .ToList();
    }

    public void AtualizarCliente(Cliente cliente)
    {
        
    }
    public void AtualizarEmailCliente(Cliente cliente)
    {
        // Creio que o cliente só pode atualizar seu email
        // Sujeito à mudanças
        string sql = "UPDATE cliente SET email = @email WHERE id_usuario = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", cliente.Id },
            { "@email", cliente.Email! }
        };

        _db.ExecutarComando(sql, parametros);
       
    }

    public void RemoverCliente(int id)
    {
        string sql = "DELETE FROM cliente WHERE id = @id";

        var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

        _db.ExecutarComando(sql, parametros);

    }
}
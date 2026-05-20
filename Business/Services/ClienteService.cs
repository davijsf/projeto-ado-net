namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;

public class ClienteService : IClienteService
{
    private readonly ClienteRepository _repository = new ClienteRepository();

    public void CadastrarCliente(Cliente cliente)
    {
        // Regra : Campos obrigatórios
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome do cliente é obrigatório.");

        if (string.IsNullOrWhiteSpace(cliente.Cpf))
            throw new ArgumentException("Cpf do cliente é obrigatório.");

        // Regra 2: Formato e validade do CPF
        if (!CpfValido(cliente.Cpf))
            throw new ArgumentException("Cpf inválido.");

        // Regra 3: Cpf já cadastrado (unicidade)
        var source = _repository.BuscarPorCpf(cliente.Cpf);
        if (source != null)
            throw new InvalidOperationException("Já existe um cliente com este CPF."); 

        // Tudo ok! 
        _repository.CadastrarCliente(cliente);
    }

    public Cliente ? BuscarPorCpf(string cpf)
    {
        // Verificação se o USER digitar '00011122233'
        // O seguinte código irá adicionar os '.' e '-' a busca.
        if (cpf.Length == 11 && cpf.All(char.IsDigit))
            cpf = $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}-{cpf[9..]}";


        return _repository.BuscarPorCpf(cpf);
    }

    public bool CpfValido(string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");
        return cpf.Length == 11 && cpf.Any(c=> c != cpf[0]);   
    }

    public List<Cliente> ListarClientes()
    {
        return _repository.ListarClientes();
    }

    public void AtualizarCliente(Cliente cliente)
    {
        // Regra 1: cliente deve existir
        var clienteExistente = _repository.BuscarPorId(cliente.Id);
        if (clienteExistente == null)
            throw new InvalidOperationException("Cliente não encontrado.");

        // Regra 2: campos obrigatórios
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome do cliente é obrigatório.");

        if (string.IsNullOrWhiteSpace(cliente.Cpf))
            throw new ArgumentException("CPF é obrigatório.");

        // Regra 3: CPF válido
        if (!CpfValido(cliente.Cpf))
            throw new ArgumentException("CPF inválido.");

        // Regra 4: se mudou o CPF, verificar se o novo já pertence a outro cliente
        if (cliente.Cpf != clienteExistente.Cpf)
        {
            var cpfEmUso = _repository.BuscarPorCpf(cliente.Cpf);
            if (cpfEmUso != null)
                throw new InvalidOperationException("Este CPF já está cadastrado para outro cliente.");
        }

        _repository.AtualizarCliente(cliente);
    }

    public void RemoverCliente(int id)
    {
        _repository.RemoverCliente(id);
    }

    public Cliente? BuscarPorIdUsuario(int idUsuario)
    {
        return _repository.BuscarPorIdUsuario(idUsuario);
    }
}

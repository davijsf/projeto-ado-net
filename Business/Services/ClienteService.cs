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

       // Remove máscara caso exista (. e -)
        string cpf = new string(cliente.Cpf.Where(char.IsDigit).ToArray());

        // Regra 1: CPF deve possuir 11 dígitos
        if (cpf.Length != 11)
            throw new ArgumentException("CPF deve conter 11 dígitos.");

        // Regra 2: Validar CPF
        if (!CpfValido(cpf))
            throw new ArgumentException("CPF inválido.");

        // Regra 3: Formatar CPF para armazenamento/exibição
        cpf = $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}-{cpf[9..]}";

        cliente.Cpf = cpf;

        // Regra 3: Cpf já cadastrado (unicidade)
        var source = _repository.BuscarPorCpf(cliente.Cpf);
        if (source != null)
            throw new InvalidOperationException("Já existe um cliente com este CPF."); 

        // Tudo ok! 
        _repository.CadastrarCliente(cliente);
    }

    public Cliente ? BuscarPorCpf(string cpf)
    {
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
        var clienteExistente = _repository.BuscarPorId(cliente.IdClient);

        if (clienteExistente == null)
            throw new InvalidOperationException("Cliente não encontrado.");

        // Atualização parcial
        clienteExistente.Nome = string.IsNullOrWhiteSpace(cliente.Nome)
            ? clienteExistente.Nome
            : cliente.Nome;

        clienteExistente.Email = string.IsNullOrWhiteSpace(cliente.Email)
            ? clienteExistente.Email
            : cliente.Email;

        // CPF: manter antigo se vazio
        string cpfOriginal = string.IsNullOrWhiteSpace(cliente.Cpf)
            ? clienteExistente.Cpf ?? ""
            : cliente.Cpf;

        // Normalizar
        string cpf = new string(
            cpfOriginal.Where(char.IsDigit).ToArray()
        );

        // Validar apenas se existir CPF
        if (!string.IsNullOrWhiteSpace(cpf))
        {
            if (cpf.Length != 11)
                throw new ArgumentException(
                    "CPF deve conter 11 dígitos.");

            if (!CpfValido(cpf))
                throw new ArgumentException(
                    "CPF inválido.");

            cpf = $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}-{cpf[9..]}";

            if (cpf != clienteExistente.Cpf)
            {
                var cpfEmUso = _repository.BuscarPorCpf(cpf);

                if (cpfEmUso != null &&
                    cpfEmUso.IdClient != cliente.IdClient)
                {
                    throw new InvalidOperationException(
                        "Este CPF já está cadastrado para outro cliente.");
                }
            }
        }

        clienteExistente.Cpf = cpf;
        _repository.AtualizarCliente(clienteExistente);
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

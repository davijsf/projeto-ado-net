namespace Services;

using Business.Interfaces;
using Data.Repositories;
using Entities;

public class VendedorService : IVendedorService
{
    private readonly VendedorRepository _repository = new VendedorRepository();

    public void CadastrarVendedor(Vendedor vendedor)
    {
        // Regra 1: Nome obrigatório
        if (string.IsNullOrWhiteSpace(vendedor.Nome))
            throw new ArgumentException(
                "Nome do vendedor é obrigatório.");

        // Regra 2: Matrícula obrigatória
        if (string.IsNullOrWhiteSpace(vendedor.Matricula))
            throw new ArgumentException(
                "Matrícula é obrigatória.");

        // Regra 3: Salário válido
        if (vendedor.Salario <= 0)
            throw new ArgumentException(
                "Salário deve ser maior que zero.");

        // Regra 4: Matrícula única
        var matriculaExistente =
            _repository.BuscarPorMatricula(vendedor.Matricula);

        if (matriculaExistente != null)
            throw new InvalidOperationException(
                "Matrícula já cadastrada.");

        _repository.CadastrarVendedor(vendedor);
    }

    public List<Vendedor> ListarVendedores()
    {
        return _repository.ListarVendedores();
    }

    public void AtualizarVendedor(Vendedor vendedor)
    {
        // Regra 1: vendedor deve existir
        var vendedorExistente =
            _repository.BuscarPorId(vendedor.IdVend);

        if (vendedorExistente == null)
            throw new InvalidOperationException(
                "Vendedor não encontrado.");

        // Atualização parcial
        vendedorExistente.Nome =
            string.IsNullOrWhiteSpace(vendedor.Nome)
            ? vendedorExistente.Nome
            : vendedor.Nome;

        vendedorExistente.Matricula =
            string.IsNullOrWhiteSpace(vendedor.Matricula)
            ? vendedorExistente.Matricula
            : vendedor.Matricula;

        vendedorExistente.Salario =
            vendedor.Salario <= 0
            ? vendedorExistente.Salario
            : vendedor.Salario;

        vendedorExistente.IdUsuario =
            vendedor.IdUsuario == null
            ? vendedorExistente.IdUsuario
            : vendedor.IdUsuario;

        // Regra 2: matrícula única (somente se alterou)
        if (!string.IsNullOrWhiteSpace(vendedorExistente.Matricula) &&
            vendedorExistente.Matricula != vendedorExistente.Matricula)
        {
            var matriculaEmUso =
                _repository.BuscarPorMatricula(
                    vendedorExistente.Matricula);

            if (matriculaEmUso != null &&
                matriculaEmUso.IdVend != vendedor.IdVend)
            {
                throw new InvalidOperationException(
                    "Esta matrícula já pertence a outro vendedor.");
            }
        }

        // Regra 3: salário válido
        if (vendedorExistente.Salario <= 0)
            throw new ArgumentException(
                "Salário deve ser maior que zero.");

        _repository.AtualizarVendedor(vendedorExistente);
    }

        public void RemoverVendedor(int id)
        {
            var vendedor =
                _repository.BuscarPorId(id);

            if (vendedor == null)
                throw new InvalidOperationException(
                    "Vendedor não encontrado.");

            _repository.RemoverVendedor(id);
        }
    }
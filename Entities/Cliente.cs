namespace Entities;

public class Cliente
{
    public int IdClient { get ; set ; }
    public string ? Nome { get ; set ; }
    public string ? Cpf { get ; set ; }
    public string ? Email { get ; set ; }
    // chave estrangeira
    public int? IdUsuario { get ; set ; }
    // propriedade de navegação
    public Usuario? Usuario { get ; set ; }
}
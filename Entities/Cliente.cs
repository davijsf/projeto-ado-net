namespace Entities;

public class Cliente : Usuario
{
    public int IdClient { get ; set ; }
    public string ? Nome { get ; set ; }
    public string ? Cpf { get ; set ; }
    public string ? Email { get ; set ; }
}
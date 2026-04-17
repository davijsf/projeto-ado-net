namespace Models;

public class Cliente : Usuario
{
    public int IdClient { get ; set ; }
    public string ? Nome { get ; set ; }
    public string ? cpf { get ; set ; }
    public string ? email { get ; set ; }
}
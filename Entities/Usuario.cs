namespace Entities;

public class Usuario
{
    public int Id { get ; set ; }
    public string ? Username { get ; set ; }
    public string ? Senha { get ; set ; }
    
    public NivelAcesso nivel { get ; set ; }

    public string ? Avatar { get ; set ; }
}
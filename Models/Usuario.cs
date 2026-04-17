namespace Models;

public class Usuario
{
    public int Id { get ; set ; }
    public string ? Username { get ; set ; }
    public string ? Senha { get ; set ; }
    public string ? nivel { get ; set ; }

    // Incerto sobre o tipo do avatar
    public string ? Avatar { get ; set ; }
}
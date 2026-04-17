namespace Models;

public class Vendedor : Usuario
{
    public int IdVend { get ; set ; }
    public string ? Nome { get ; set ; }
    public string ? Matricula { get ; set ; }
    public double Salario { get ; set ; }

    // chave estrangeira
    public int? IdUsuario { get ; set ; }
    // propriedade de navegação
    public Usuario? Usuario { get ; set ; }
}
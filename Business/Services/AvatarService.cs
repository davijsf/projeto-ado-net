namespace Services;

public static class AvatarService
{   

    // Pasta relativa à raiz do projeto
    private static readonly string PastaAvatars = Path.GetFullPath(
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../avatars")  
    );

    private static readonly string[] ExtensoesPermitidas = { ".jpg", ".jpeg", ".png", ".webp"};

    public static string? SalvarAvatar (int usuarioId, string caminhoOrigem)
    {
        // Remove espaços e caracteres invisíveis comuns ao colar caminhos no Windows
        caminhoOrigem = caminhoOrigem.Trim().Trim('\u202a', '\u202c', '"');

        if (!File.Exists(caminhoOrigem))
        {
            Console.WriteLine("Arquivo não encontrado.");
            return null;
        }

        string extensao = Path.GetExtension(caminhoOrigem).ToLower();
        if (!ExtensoesPermitidas.Contains(extensao))
        {
            Console.WriteLine("Formato não suportado. Use JPG, JPEG, PNG ou WEBP");
            return null;
        }

        // Garante que a pasta existe
        Directory.CreateDirectory(PastaAvatars);

        // Nome do arquivo baseado no Id do usuário
        string nomeArquivo = $"avatar_{usuarioId}{extensao}";
        string caminhoDestino = Path.Combine(PastaAvatars, nomeArquivo);

        File.Copy(caminhoOrigem, caminhoDestino, overwrite: true);

        Console.WriteLine($"Avatar salvo em: {caminhoDestino}");

        // retorna o nome do arquivo para salvar no banco
        return nomeArquivo;

    }
}
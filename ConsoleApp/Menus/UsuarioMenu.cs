// // namespace ConsoleApp.Menus;

// // using Entities;
// // using Services;

// // public class UsuarioMenu
// // {
// //     private readonly UsuarioService _usuarioService;

// //     public UsuarioMenu(UsuarioService usuarioService)
// //     {
// //         _usuarioService = usuarioService;
// //     }

// //     public void ExibirMenu()
// //     {
// //         Usuario? usuarioLogado = null;
// //         bool loop = true;

// //         while (loop)
// //         {
// //             Console.Clear();

// //             if (usuarioLogado == null)
// //             {
// //                 loop = MenuPublico(ref usuarioLogado);
// //             }
// //             else
// //             {
// //                 loop = MenuPrivado(ref usuarioLogado);
// //             }
// //         }
// //     }

// //     // ===================== MENU PÚBLICO =====================
// //     private bool MenuPublico(ref Usuario? usuarioLogado)
// //     {
// //         Console.WriteLine("=== MENU USUÁRIO (PÚBLICO) ===");
// //         Console.WriteLine("1. Cadastrar usuário");
// //         Console.WriteLine("2. Listar usuários");
// //         Console.WriteLine("3. Login");
// //         Console.WriteLine("0. Sair");

// //         Console.Write("Digite: ");
// //         string opcao = Console.ReadLine()!;

// //         switch (opcao)
// //         {
// //             case "1": CadastrarUsuario(); break;
// //             case "2": ListarUsuarios(); break;
// //             case "3": usuarioLogado = Login(); break;
// //             case "0": return false;

// //             default:
// //                 Console.WriteLine("Opção inválida!");
// //                 Console.ReadKey();
// //                 break;
// //         }

// //         return true;
// //     }

// //     // ===================== MENU PRIVADO =====================
// //     private bool MenuPrivado(ref Usuario? usuarioLogado)
// //     {
// //         Console.WriteLine("=== MENU USUÁRIO (LOGADO) ===");
// //         Console.WriteLine($"Usuário: {usuarioLogado!.Username}");
// //         Console.WriteLine();
// //         Console.WriteLine("1. Meu perfil");
// //         Console.WriteLine("2. Atualizar usuário");
// //         Console.WriteLine("3. Alterar nível de acesso (Admin)");
// //         Console.WriteLine("4. Deletar usuário (Admin)");
// //         Console.WriteLine("5. Buscar usuário");
// //         Console.WriteLine("6. Upload avatar");
// //         Console.WriteLine("7. Logout");
// //         Console.WriteLine("0. Sair");

// //         Console.Write("Digite: ");
// //         string opcao = Console.ReadLine()!;

// //         switch (opcao)
// //         {
// //             case "1": ExibirPerfil(usuarioLogado); break;
// //             case "2": AtualizarUsuario(usuarioLogado); break;

// //             case "3":
// //                 if (usuarioLogado.nivel == NivelAcesso.Admin)
// //                     AlterarNivelAcesso();
// //                 else
// //                     AcessoNegado();
// //                 break;

// //             case "4":
// //                 if (usuarioLogado.nivel == NivelAcesso.Admin)
// //                     DeletarUsuario();
// //                 else
// //                     AcessoNegado();
// //                 break;

// //             case "5": BuscarUsuario(); break;
// //             case "6": UploadAvatar(usuarioLogado); break;

// //             case "7":
// //                 usuarioLogado = null;
// //                 Console.WriteLine("Logout realizado!");
// //                 Console.ReadKey();
// //                 break;

// //             case "0": return false;

// //             default:
// //                 Console.WriteLine("Opção inválida!");
// //                 Console.ReadKey();
// //                 break;
// //         }

// //         return true;
// //     }

// //     // ===================== FUNÇÕES =====================

// //     public void CadastrarUsuario()
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== CADASTRAR USUÁRIO ===");

// //         Console.Write("Username: ");
// //         string username = Console.ReadLine()!;

// //         Console.Write("Senha: ");
// //         string senha = _usuarioService.CriptografarSenha(Console.ReadLine()!);

// //         Console.Write("Nível (Comum/Admin): ");
// //         NivelAcesso nivel;
// //         while (!Enum.TryParse(Console.ReadLine(), true, out nivel))
// //             Console.Write("Inválido. Digite Comum ou Admin: ");

// //         Console.Write("Avatar: ");
// //         string avatar = Console.ReadLine()!;

// //         var existente = _usuarioService.BuscarUsuarioPorUsername(username);

// //         if (existente != null)
// //         {
// //             Console.WriteLine("Usuário já existe!");
// //             Console.ReadKey();
// //             return;
// //         }

// //         var usuario = new Usuario
// //         {
// //             Username = username,
// //             Senha = senha,
// //             nivel = nivel,
// //             Avatar = avatar
// //         };

// //         _usuarioService.CadastrarUsuario(usuario);

// //         Console.WriteLine("Usuário cadastrado com sucesso!");
// //         Console.ReadKey();
// //     }

// //     public Usuario? Login()
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== LOGIN ===");

// //         Console.Write("Username: ");
// //         string username = Console.ReadLine()!;

// //         Console.Write("Senha: ");
// //         string senha = LerSenha();

// //         var usuario = _usuarioService.Login(username, senha);

// //         if (usuario == null)
// //         {
// //             Console.WriteLine("Login inválido!");
// //             Console.ReadKey();
// //             return null;
// //         }

// //         Console.WriteLine($"Bem-vindo {usuario.Username}!");
// //         Console.ReadKey();
// //         return usuario;
// //     }

// //     private void ListarUsuarios()
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== LISTA DE USUÁRIOS ===");

// //         var usuarios = _usuarioService.ListarUsuarios();

// //         foreach (var u in usuarios)
// //         {
// //             Console.WriteLine($"{u.Id} - {u.Username} - {u.nivel}");
// //         }

// //         Console.ReadKey();
// //     }

// //     private void AtualizarUsuario(Usuario usuario)
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== ATUALIZAR USUÁRIO ===");

// //         Console.Write("Novo username (enter para manter): ");
// //         string username = Console.ReadLine()!;

// //         usuario.Username = string.IsNullOrWhiteSpace(username)
// //             ? usuario.Username
// //             : username;

// //         _usuarioService.AtualizarUsuario(usuario);

// //         Console.WriteLine("Atualizado com sucesso!");
// //         Console.ReadKey();
// //     }

// //     public void BuscarUsuario()
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== BUSCAR USUÁRIO ===");

// //         Console.Write("Username: ");
// //         string username = Console.ReadLine()!;

// //         var usuario = _usuarioService.BuscarUsuarioPorUsername(username);

// //         if (usuario == null)
// //         {
// //             Console.WriteLine("Não encontrado!");
// //             Console.ReadKey();
// //             return;
// //         }

// //         Console.WriteLine($"ID: {usuario.Id}");
// //         Console.WriteLine($"Username: {usuario.Username}");
// //         Console.WriteLine($"Nível: {usuario.nivel}");
// //         Console.WriteLine($"Avatar: {usuario.Avatar}");

// //         Console.ReadKey();
// //     }

// //     public void AlterarNivelAcesso()
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== ALTERAR NÍVEL ===");

// //         Console.Write("ID: ");
// //         int id = int.Parse(Console.ReadLine()!);

// //         Console.Write("Novo nível (0 Comum / 1 Admin): ");
// //         NivelAcesso nivel = (NivelAcesso)int.Parse(Console.ReadLine()!);

// //         _usuarioService.AlterarNivelAcesso(id, nivel);

// //         Console.WriteLine("Alterado!");
// //         Console.ReadKey();
// //     }

// //     public void UploadAvatar(Usuario usuario)
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== UPLOAD AVATAR ===");

// //         Console.Write("Caminho: ");
// //         string caminho = Console.ReadLine()!;

// //         string? nome = AvatarService.SalvarAvatar(usuario.Id, caminho);

// //         if (nome != null)
// //         {
// //             _usuarioService.UploadAvatar(usuario.Id, nome);
// //             usuario.Avatar = nome;
// //         }

// //         Console.WriteLine("Avatar atualizado!");
// //         Console.ReadKey();
// //     }

// //     public void DeletarUsuario()
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== DELETAR USUÁRIO ===");

// //         Console.Write("ID: ");
// //         int id = int.Parse(Console.ReadLine()!);

// //         _usuarioService.DeletarUsuario(id);

// //         Console.WriteLine("Deletado!");
// //         Console.ReadKey();
// //     }

// //     public void ExibirPerfil(Usuario usuario)
// //     {
// //         Console.Clear();
// //         Console.WriteLine("=== PERFIL ===");

// //         Console.WriteLine($"ID: {usuario.Id}");
// //         Console.WriteLine($"Username: {usuario.Username}");
// //         Console.WriteLine($"Nível: {usuario.nivel}");
// //         Console.WriteLine($"Avatar: {usuario.Avatar}");

// //         Console.ReadKey();
// //     }

// //     private void AcessoNegado()
// //     {
// //         Console.WriteLine("Acesso negado!");
// //         Console.ReadKey();
// //     }

// //     private string LerSenha()
// //     {
// //         string senha = "";

// //         while (true)
// //         {
// //             var tecla = Console.ReadKey(true);

// //             if (tecla.Key == ConsoleKey.Enter)
// //             {
// //                 Console.WriteLine();
// //                 break;
// //             }
// //             else if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
// //             {
// //                 senha = senha[..^1];
// //                 Console.Write("\b \b");
// //             }
// //             else
// //             {
// //                 senha += tecla.KeyChar;
// //                 Console.Write("*");
// //             }
// //         }

// //         return senha;
// //     }
// }
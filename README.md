# 📚 Livraria ADO.NET

Sistema de gerenciamento de livraria desenvolvido em **C# com ADO.NET**, utilizando **MySQL** como banco de dados local. O projeto simula o fluxo completo de uma livraria: cadastro de livros, autores, clientes, vendedores e registro de vendas — tudo via aplicação console.

---

## 🗂️ Estrutura do Projeto

```
ADO-Net-Solution.slnx
│
├── ConsoleApp/               # Ponto de entrada e menus interativos
│   ├── Application.cs
│   └── Menus/
│       ├── AutorMenu.cs
│       ├── ClienteMenu.cs
│       ├── LivroMenu.cs
│       ├── UsuarioMenu.cs
│       ├── VendaMenu.cs
│       └── VendedorMenu.cs
│
├── Business/
│   ├── Interfaces/           # Contratos de serviço (IUsuarioService, ILivroService...)
│   └── Services/             # Lógica de negócio (UsuarioService, LivroService...)
│
├── Data/
│   ├── DataBaseConnection.cs         # Gerenciamento de conexão MySQL via ADO.NET
│   ├── Repositories/                 # Acesso a dados por entidade
│   │   ├── Interfaces/               # Contratos de repositório
│   │   └── RepositoryBase.cs         # Base com métodos reutilizáveis
│   ├── Seeds/
│   │   └── seed_livraria.csx         # Script de seed (dotnet-script)
│   └── script_livraria_ado_net.sql   # Script de criação do banco
│
├── Entities/                 # Modelos de domínio
│   ├── Autor.cs
│   ├── Cliente.cs
│   ├── ItemVenda.cs
│   ├── Livro.cs
│   ├── NivelAcesso.cs        # Enum: Comum | Admin
│   ├── Usuario.cs
│   ├── Venda.cs
│   └── Vendedor.cs
│
└── docs/
    ├── Diagrama_livraria.pdf
    └── funcionalidades.md
```

---

## ⚙️ Tecnologias

| Tecnologia | Uso |
|---|---|
| C# / .NET 10 | Linguagem e plataforma |
| ADO.NET | Acesso ao banco de dados |
| MySQL | Banco de dados relacional local |
| BCrypt.Net-Next | Hash e verificação de senhas |
| dotnet-script | Execução do seed `.csx` |

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas inspirada no padrão **MVC**:

- **Entities** — modelos de domínio puros, sem dependências externas
- **Data** — repositórios responsáveis exclusivamente pelo acesso ao banco (ADO.NET)
- **Business** — serviços com a lógica de negócio, orquestrando os repositórios
- **ConsoleApp** — interface com o usuário (menus interativos no terminal)

### 🔄 Fluxo de Dados

A requisição parte sempre do menu no `ConsoleApp`, passa pelo `Service` que aplica as regras de negócio, e chega ao `Repository` que executa o SQL no banco — e o retorno percorre o mesmo caminho de volta.

```
┌─────────────────────────────────────────────────────────────┐
│                        ConsoleApp                           │
│                  (LivroMenu, UsuarioMenu)                   │
│          Entrada do usuário / Exibição de resultados        │
└──────────────────────────┬──────────────────────────────────┘
                           │  chama
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                    Business / Services                      │
│         (LivroService, UsuarioService, VendaService...)     │
│    Valida regras de negócio, orquestra chamadas ao Data     │
└──────────────────────────┬──────────────────────────────────┘
                           │  chama
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                  Data / Repositories                        │
│      (LivroRepository, UsuarioRepository, VendaRepository) │
│         Executa queries SQL via ADO.NET no MySQL            │
└──────────────────────────┬──────────────────────────────────┘
                           │  lê/escreve
                           ▼
                    [ MySQL Database ]
```

**Exemplo de fluxo — Login:**

```
UsuarioMenu.Login()
  → UsuarioService.Login(username, senha)
    → UsuarioRepository.Login(username)        # busca o hash do banco
      ← retorna Usuario com hash salvo
    → BCrypt.Verify(senha, hash)               # valida a senha
  ← retorna Usuario? (null se inválido)
← exibe "Bem vindo!" ou "Credenciais inválidas"
```

---

## 👤 Controle de Acesso

O controle de nível de acesso é feito no momento do login, no `Application.cs`. Após autenticação, o menu exibido varia conforme o nível do usuário logado:

### 🛡️ Admin — acesso completo
- Gerenciar Livros (cadastrar, listar, atualizar, remover)
- Gerenciar Clientes (cadastrar, listar, atualizar, remover)
- Gerenciar Vendedores (cadastrar, listar, atualizar, remover)
- Gerenciar Autores (cadastrar, listar, atualizar, remover)
- Registrar e consultar Vendas
- Gerenciar Usuários (cadastrar, alterar nível de acesso, deletar)

### 👤 Comum — acesso restrito
- Realizar compras (carrinho de compras)
- Visualizar o próprio perfil

---

## 🛒 Carrinho de Compras

O carrinho funciona em memória durante a sessão de compra. O fluxo é:

```
Nova Venda
  → Adicionar livros ao carrinho (busca por nome)
  → Sistema verifica estoque disponível
  → Confirmar compra → salva Venda + ItenVenda no banco
```

As entidades `Carrinho` e `ItemCarrinho` existem apenas em memória e nunca são persistidas no banco.

---

## 🗃️ Entidades

| Entidade | Descrição |
|---|---|
| `Usuario` | Usuário do sistema com nível de acesso (`Comum` ou `Admin`) |
| `Cliente` | Cliente que realiza compras |
| `Vendedor` | Vendedor vinculado opcionalmente a um `Usuario` |
| `Autor` | Autor dos livros |
| `Livro` | Livro disponível na livraria |
| `Venda` | Registro de compra (data + valor total) |
| `ItemVenda` | Item individual de uma venda (livro + quantidade + subtotal) |

---

## 🔧 Configuração

As credenciais do banco de dados são lidas de um arquivo `appsettings.json` na raiz do `ConsoleApp`. Esse arquivo **não é commitado** — crie o seu a partir do exemplo:

```bash
cp ConsoleApp/appsettings.example.json ConsoleApp/appsettings.json
```

Edite com suas credenciais locais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=livraria_ado_net;Uid=root;Pwd=sua_senha_aqui;"
  }
}
```

> ⚠️ O `appsettings.json` já está no `.gitignore`. Nunca o commite com dados reais.

## 🚀 Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- MySQL rodando localmente
- [dotnet-script](https://github.com/dotnet-script/dotnet-script) (para o seed)

### 1. Criar o banco de dados

Execute o script SQL no seu cliente MySQL:

```bash
mysql -u root -p < Data/script_livraria_ado_net.sql
```

### 2. Popular o banco (seed)

```bash
cd Data/Seeds

dotnet script seed_livraria.csx
```

### 3. Rodar a aplicação

```bash
dotnet run --project ConsoleApp
```

---

## 🔑 Credenciais de Teste (seed)

| Username | Senha | Nível |
|---|---|---|
| `admin` | `admin123` | Admin |
| `joao.vendas` | `joao123` | Comum |
| `maria.vendas` | `maria123` | Comum |
| `carlos.v` | `carlos123` | Comum |
| `ana.v` | `ana123` | Comum |

---

## 📄 Documentação

- [`docs/Diagrama_livraria.pdf`](docs/Diagrama_livraria.pdf) — Diagrama do banco de dados
- [`docs/funcionalidades.md`](docs/funcionalidades.md) — Descrição detalhada das funcionalidades

-- ===========================
-- BANCO DE DADOS
-- ===========================
CREATE DATABASE IF NOT EXISTS livraria_ado_net;
USE livraria_ado_net;

-- ===========================
-- TABELA USUARIO
-- (sem dependências externas)
-- ===========================
CREATE TABLE IF NOT EXISTS usuario (
    id       INT          AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50)  NOT NULL UNIQUE,
    senha    VARCHAR(255) NOT NULL,
    nivel    VARCHAR(20)  NOT NULL,   -- 'admin' ou 'comum'
    avatar   VARCHAR(255)
);

-- ===========================
-- TABELA AUTOR
-- (sem dependências externas)
-- ===========================
CREATE TABLE IF NOT EXISTS autor (
    id            INT         AUTO_INCREMENT PRIMARY KEY,
    nome          VARCHAR(100) NOT NULL,
    nacionalidade VARCHAR(50)
);

-- ===========================
-- TABELA CLIENTE
-- (depende de: usuario)
-- ===========================
CREATE TABLE IF NOT EXISTS cliente (
    id         INT          AUTO_INCREMENT PRIMARY KEY,
    nome       VARCHAR(100) NOT NULL,
    cpf        VARCHAR(14)  NOT NULL UNIQUE,
    email      VARCHAR(100),
    id_usuario INT          UNIQUE,
    CONSTRAINT fk_cliente_usuario
        FOREIGN KEY (id_usuario) REFERENCES usuario(id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

-- ===========================
-- TABELA VENDEDOR
-- (depende de: usuario)
-- ===========================
CREATE TABLE IF NOT EXISTS vendedor (
    id         INT           AUTO_INCREMENT PRIMARY KEY,
    nome       VARCHAR(100)  NOT NULL,
    matricula  VARCHAR(20)   NOT NULL UNIQUE,
    salario    DECIMAL(10,2) NOT NULL,
    id_usuario INT           UNIQUE,
    CONSTRAINT fk_vendedor_usuario
        FOREIGN KEY (id_usuario) REFERENCES usuario(id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

-- ===========================
-- TABELA LIVRO
-- (depende de: autor)
-- ===========================
CREATE TABLE IF NOT EXISTS livro (
    id       INT           AUTO_INCREMENT PRIMARY KEY,
    titulo   VARCHAR(150)  NOT NULL,
    preco    DECIMAL(10,2) NOT NULL,
    estoque  INT           NOT NULL,
    id_autor INT,
    CONSTRAINT fk_livro_autor
        FOREIGN KEY (id_autor) REFERENCES autor(id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

-- ===========================
-- TABELA VENDA
-- (depende de: cliente, vendedor)
-- ===========================
CREATE TABLE IF NOT EXISTS venda (
    id          INT           AUTO_INCREMENT PRIMARY KEY,
    data        DATETIME      NOT NULL,
    total       DECIMAL(10,2) NOT NULL,
    id_cliente  INT           NOT NULL,
    id_vendedor INT           NULL,        -- NULL permite venda sem vendedor
    CONSTRAINT fk_venda_cliente
        FOREIGN KEY (id_cliente)  REFERENCES cliente(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT fk_venda_vendedor
        FOREIGN KEY (id_vendedor) REFERENCES vendedor(id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

-- ===========================
-- TABELA ITEMVENDA
-- (depende de: livro, venda)  <- deve ser a ÚLTIMA
-- ===========================
CREATE TABLE IF NOT EXISTS itemvenda (
    id         INT           AUTO_INCREMENT PRIMARY KEY,
    quantidade INT           NOT NULL,
    subtotal   DECIMAL(10,2) NOT NULL,
    id_livro   INT           NOT NULL,
    id_venda   INT           NOT NULL,
    CONSTRAINT fk_itemvenda_livro
        FOREIGN KEY (id_livro) REFERENCES livro(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT fk_itemvenda_venda
        FOREIGN KEY (id_venda) REFERENCES venda(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
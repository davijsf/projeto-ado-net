CREATE DATABASE livraria_ado_net;
USE livraria_ado_net;

-- ===========================
-- TABELA CLIENTE
-- ===========================
CREATE TABLE cliente (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(14) NOT NULL UNIQUE,
    email VARCHAR(100),
    id_usuario INT UNIQUE,
    CONSTRAINT fk_cliente_usuario
        FOREIGN KEY (id_usuario) REFERENCES usuario(id)
);

-- ===========================
-- TABELA VENDEDOR
-- ===========================
CREATE TABLE vendedor (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    matricula VARCHAR(20) NOT NULL UNIQUE,
    salario DECIMAL(10,2) NOT NULL,
	id_usuario INT UNIQUE, 
    CONSTRAINT fk_vendedor_usuario
	FOREIGN KEY (id_usuario) REFERENCES usuario(id)
);

-- ===========================
-- TABELA AUTOR
-- ===========================
CREATE TABLE autor (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    nacionalidade VARCHAR(50)
);

-- ===========================
-- TABELA LIVRO
-- ===========================
CREATE TABLE livro (
    id INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(150) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    estoque INT NOT NULL,

    id_autor INT,
    CONSTRAINT fk_livro_autor
        FOREIGN KEY (id_autor) REFERENCES autor(id)
        ON DELETE SET NULL
        ON UPDATE CASCADE

);

-- ===========================
-- TABELA VENDA
-- ===========================
CREATE TABLE venda (
    id INT AUTO_INCREMENT PRIMARY KEY,
    data DATETIME NOT NULL,
    total DECIMAL(10,2) NOT NULL,

    id_cliente INT NOT NULL,
    id_vendedor INT NOT NULL,

    CONSTRAINT fk_venda_cliente
        FOREIGN KEY (id_cliente) REFERENCES cliente(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT fk_venda_vendedor
        FOREIGN KEY (id_vendedor) REFERENCES vendedor(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

-- ===========================
-- TABELA ITEMVENDA
-- ===========================
CREATE TABLE itemvenda (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quantidade INT NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,

    id_livro INT NOT NULL,
    id_venda INT NOT NULL,

    CONSTRAINT fk_itemvenda_livro
        FOREIGN KEY (id_livro) REFERENCES livro(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT fk_itemvenda_venda
        FOREIGN KEY (id_venda) REFERENCES venda(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

-- ===========================
-- TABELA USUARIO
-- ===========================
CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    nivel VARCHAR(20) NOT NULL, -- admin ou comum
    avatar VARCHAR(255)
);
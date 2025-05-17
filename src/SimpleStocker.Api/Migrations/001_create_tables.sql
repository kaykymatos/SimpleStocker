-- 001_create_tables.sql

-- ==============================
-- Tabela: Categories
-- ==============================
CREATE TABLE Categories (
    Id BIGSERIAL PRIMARY KEY,                                -- Identificador único da categoria
    Name VARCHAR(100) NOT NULL,                              -- Nome da categoria
    Description TEXT,                                        -- Descrição da categoria
    CreatedDate TIMESTAMP NOT NULL DEFAULT NOW(),            -- Data de criação do registro
    UpdatedDate TIMESTAMP                                    -- Data da última atualização
);

-- ==============================
-- Tabela: Clients
-- ==============================
CREATE TABLE Clients (
    Id BIGSERIAL PRIMARY KEY,                                -- Identificador único do cliente
    Name VARCHAR(150) NOT NULL,                              -- Nome completo do cliente
    Email VARCHAR(150) UNIQUE NOT NULL,                      -- E-mail do cliente (único)
    PhoneNumer VARCHAR(20),                                  -- Telefone do cliente
    Address VARCHAR(255),                                    -- Endereço (rua, avenida etc.)
    AddressNumber VARCHAR(10),                               -- Número da residência
    Active BOOLEAN NOT NULL DEFAULT TRUE,                    -- Define se o cliente está ativo
    BirthDate DATE NOT NULL,                                 -- Data de nascimento
    CreatedDate TIMESTAMP NOT NULL DEFAULT NOW(),            -- Data de criação do registro
    UpdatedDate TIMESTAMP                                    -- Data da última atualização
);

-- ==============================
-- Tabela: Products
-- ==============================
CREATE TABLE Products (
    Id BIGSERIAL PRIMARY KEY,                                -- Identificador único do produto
    Name VARCHAR(100) NOT NULL,                              -- Nome do produto
    Description TEXT,                                        -- Descrição do produto
    QuantityStock DOUBLE PRECISION NOT NULL DEFAULT 0,       -- Quantidade atual em estoque
    UnityOfMeasurement VARCHAR(10) NOT NULL,                 -- Unidade de medida (ex: KG, L, UN)
    Price NUMERIC(10, 2) NOT NULL,                           -- Preço unitário do produto
    CategoryId BIGINT NOT NULL,                              -- FK para a tabela de categorias
    CreatedDate TIMESTAMP NOT NULL DEFAULT NOW(),            -- Data de criação
    UpdatedDate TIMESTAMP,                                   -- Data da última atualização
    CONSTRAINT fk_product_category FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id) ON DELETE CASCADE
);

-- ==============================
-- Tabela: Sales
-- ==============================
CREATE TABLE Sales (
    Id BIGSERIAL PRIMARY KEY,                                -- Identificador único da venda
    CustomerId BIGINT NOT NULL,                              -- FK para o cliente que realizou a compra
    Discount NUMERIC(10, 2) NOT NULL DEFAULT 0,              -- Desconto aplicado à venda
    PaymentMethod SMALLINT NOT NULL,                         -- Método de pagamento (mapeado via enum EPaymentMethod)
    Status SMALLINT NOT NULL,                                -- Status da venda (mapeado via enum ESaleStatus)
    CreatedDate TIMESTAMP NOT NULL DEFAULT NOW(),            -- Data de criação da venda
    UpdatedDate TIMESTAMP,                                   -- Última atualização da venda
    CONSTRAINT fk_sale_client FOREIGN KEY (CustomerId)
        REFERENCES Clients(Id) ON DELETE CASCADE
);

-- ==============================
-- Tabela: SaleItems
-- ==============================
CREATE TABLE SaleItems (
    Id BIGSERIAL PRIMARY KEY,                                -- Identificador único do item da venda
    SaleId BIGINT NOT NULL,                                  -- FK para a venda
    ProductId BIGINT NOT NULL,                               -- FK para o produto vendido
    Quantity DOUBLE PRECISION NOT NULL DEFAULT 1,            -- Quantidade vendida do produto
    UnityPrice NUMERIC(10, 2) NOT NULL,                      -- Preço unitário no momento da venda
    CreatedDate TIMESTAMP NOT NULL DEFAULT NOW(),            -- Data de criação do item da venda
    UpdatedDate TIMESTAMP,                                   -- Última atualização
    CONSTRAINT fk_saleitem_sale FOREIGN KEY (SaleId)
        REFERENCES Sales(Id) ON DELETE CASCADE,
    CONSTRAINT fk_saleitem_product FOREIGN KEY (ProductId)
        REFERENCES Products(Id) ON DELETE CASCADE
);

-- ==============================
-- Comentários para enums
-- ==============================
-- PaymentMethod:
-- 0 = Cash
-- 1 = CreditCard
-- 2 = DebitCard
-- 3 = Pix
-- 4 = BankTransfer
-- 5 = Boleto
-- 6 = DigitalWallet
-- 7 = CryptoCurrency

-- SaleStatus:
-- 0 = Pending
-- 1 = Confirmed
-- 2 = Shipped
-- 3 = Completed
-- 4 = Cancelled

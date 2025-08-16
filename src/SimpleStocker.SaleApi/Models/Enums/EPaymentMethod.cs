namespace SimpleStocker.SaleApi.Models.Enums
{
    public enum EPaymentMethod
    {
        Cash = 0,             // Dinheiro
        CreditCard = 1,       // Cartão de crédito
        DebitCard = 2,        // Cartão de débito
        Pix = 3,              // Pagamento instantâneo (Brasil)
        BankTransfer = 4,     // Transferência bancária
        Boleto = 5,           // Boleto bancário
        DigitalWallet = 6,    // Carteira digital (ex: Mercado Pago, PayPal)
        CryptoCurrency = 7    // Moeda digital (ex: Bitcoin)
    }
}

namespace SimpleStocker.SaleApi.Models.Enums
{
    public enum EPaymentMethod
    {
        Cash,             // Dinheiro
        CreditCard,       // Cartão de crédito
        DebitCard,        // Cartão de débito
        Pix,              // Pagamento instantâneo (Brasil)
        BankTransfer,     // Transferência bancária
        Boleto,           // Boleto bancário
        DigitalWallet,    // Carteira digital (ex: Mercado Pago, PayPal)
        CryptoCurrency    // Moeda digital (ex: Bitcoin)
    }
}

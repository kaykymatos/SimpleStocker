namespace SimpleStocker.SaleApi.Models.Enums
{
    public enum ESaleStatus
    {
        Pending = 0,        // Aguardando pagamento ou confirmação
        Confirmed = 1,      // Venda confirmada e aprovada
        Shipped = 2,        // Produto enviado (se aplicável)
        Completed = 3,      // Venda finalizada com sucesso
        Cancelled = 4       // Venda cancelada
    }

}

export enum ESaleStatus {
  Pending, // Aguardando pagamento ou confirmação
  Confirmed, // Venda confirmada e aprovada
  Shipped, // Produto enviado (se aplicável)
  Completed, // Venda finalizada com sucesso
  Cancelled, // Venda cancelada
}

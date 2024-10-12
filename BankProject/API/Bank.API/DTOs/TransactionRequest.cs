namespace Bank.API.DTOs
{
    public class TransactionRequest
    {
        public Guid Account { get; set; }

        public string Amount { get; set; }
    }

}

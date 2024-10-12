namespace Bank.API.DTOs
{
    public class TransferRequest
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Amount { get; set; }
    }
}

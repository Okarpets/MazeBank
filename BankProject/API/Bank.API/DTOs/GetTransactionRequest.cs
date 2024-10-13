namespace BANK.API.DTOs
{
    public class GetTransactionRequest
    {
        public Guid accountId { get; set; }

        public int filter { get; set; }
    }
}

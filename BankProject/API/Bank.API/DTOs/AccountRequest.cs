namespace Bank.API.DTOs
{
    public class AccountRequest
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public Guid UserId { get; set; }
    }
}

namespace Bank.API.DTOs
{
    public class ShopRequest
    {
        public string FromAccount { get; set; }

        public string ToAccount { get; set; }

        public string Amount { get; set; }

        public string Email { get; set; }

        public Guid OrderId { get; set; }
    }
}

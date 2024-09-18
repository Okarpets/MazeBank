namespace API.DataLayer.Models
{
    public class UserModel : IEntityModel
    {

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<AccountModel> Accounts { get; set; } = new List<AccountModel>();
    }
}

namespace API.DataLayer.Entities
{
    public class UserEntity : IEntity
    {

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}

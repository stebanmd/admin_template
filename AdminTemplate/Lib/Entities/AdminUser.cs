namespace Lib.Entities
{
    public class AdminUser : BaseEntity
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public bool Enabled { get; set; }
        public string Password { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
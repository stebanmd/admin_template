namespace Lib.Entities
{
    internal class MenuAccess : BaseEntity
    {
        public int ProfileId { get; set; }
        public string Menu { get; set; }
        public bool Allowed { get; set; }
    }
}
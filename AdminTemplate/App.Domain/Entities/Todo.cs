namespace App.Domain.Entities
{
    public class Todo : BaseEntity
    {
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
}
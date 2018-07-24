namespace App.Domain.Filters
{
    public class BaseFilter
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IncludeBase64Field { get; set; }
    }
}
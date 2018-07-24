using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Filters
{
    public class TodoFilter
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public bool? IsCompleted { get; set; }
    }
}

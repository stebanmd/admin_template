using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Dtos
{
    public class TodoFilterDto
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public bool? IsCompleted { get; set; }
    }
}

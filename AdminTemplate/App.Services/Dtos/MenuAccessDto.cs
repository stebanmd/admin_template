using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Dtos
{
    public class MenuAccessDto: BaseDto
    {
        public int ProfileId { get; set; }

        public string Menu { get; set; }

        public bool Allowed { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }
    }
}

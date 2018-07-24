using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Services.Dtos
{
    public class AdminUserDto: BaseDto
    {
        [Range(1, Int32.MaxValue)]
        public int ProfileId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Photo { get; set; }

        public bool Enabled { get; set; }

        public string Password { get; set; }

        public virtual ProfileDto Profile { get; set; }
    }
}

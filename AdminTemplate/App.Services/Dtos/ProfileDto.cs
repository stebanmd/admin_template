using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Services.Dtos
{
    public class ProfileDto : BaseDto
    {
        [Required]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public IEnumerable<MenuAccessDto> ProfileMenuAccess { get; set; }
    }
}
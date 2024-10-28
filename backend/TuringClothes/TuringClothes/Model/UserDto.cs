using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace TuringClothes.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]

        [DefaultValue("user")]
        public string Role { get; set; }
    }
}

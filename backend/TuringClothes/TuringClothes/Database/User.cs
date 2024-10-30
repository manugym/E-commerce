using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuringClothes.Database
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        /*[Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public long Id { get; set; }
 
        public string Name { get; set; }
  
        public string Surname { get; set; }

        public string Email { get; set; }
     
        public string Password { get; set; }
 
        public string Address { get; set; }
   

        [DefaultValue("user")]
        public string Role { get; set; }



    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }
        [Required]
        public string Username { get; set; }
        
        public string FullName { get; set; }
        
        public DateTime BirthdayDate { get; set; }

    }
}

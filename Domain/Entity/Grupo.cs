using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Group
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    
    }
}

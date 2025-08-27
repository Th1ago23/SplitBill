using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(75, ErrorMessage = "Nome do grupo deve conter até 75 caracteres.")]
        public string Name { get; set; }
        public int LeaderId { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    }
}

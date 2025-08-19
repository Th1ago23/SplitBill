using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        
        public byte[] PasswordHash { get; set; }
        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public DateTime BirthdayDate { get; set; }

        public virtual ICollection<Group>? Groups { get; set; }
        public virtual ICollection<Expense>? ParticipatedInExpenses { get; set; }

    }
}

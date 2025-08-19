using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int PaidByUserId { get; set; }
        public int GroupId { get; set; }

        [ForeignKey("PaidByUserId")]
        public virtual User Payer { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public virtual ICollection<User> Participants { get; set; } = new List<User>();

    }
}

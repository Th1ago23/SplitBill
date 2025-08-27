using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public DateTime Birthday { get; private set; }

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Birthday.Year;
                if (Birthday.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        public bool HasMinimumAge()
        {
            const int minimumAge = 12;
            return Age >= minimumAge;
        }

        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
        public virtual ICollection<Expense> ParticipatedInExpenses { get; set; } = new List<Expense>();

        public User(string name, string username, string email, string passwordHash, DateTime birthday)
        {
            FullName = name;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Birthday = birthday;
        }
        public User()
        { }

    }
}

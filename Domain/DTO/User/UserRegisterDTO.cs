using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.User
{
    public record UserRegisterDTO(
        [Required]
        [EmailAddress]
        string EmailAddress,

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage ="A senha deve ter entre 6 a 100 caracteres")]
        string Password,

        [Required]
        string Username,

        [Required]
        string Fullname,
        [Required]
        DateTime BirthDay,

        [Required]
        string PhoneNumber
        )
    { }
}

using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.User
{
    public record UserLoginDTO(
        [Required]
        string email,

        [Required]
        string password
        )
    { }
}

using System.ComponentModel.DataAnnotations;

namespace Application.DTO.User
{
    public record UserLoginDTO(
        [Required]
        string email,

        [Required]
        string password
        )
    { }
}

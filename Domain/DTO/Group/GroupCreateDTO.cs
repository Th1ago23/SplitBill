using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Group
{
    public record GroupCreateDTO(
        [Required]
        [StringLength(50, ErrorMessage ="O nome do grupo deve ter até 50 caracteres")]
        string name, int leaderId, bool isPublic)
    { }
}

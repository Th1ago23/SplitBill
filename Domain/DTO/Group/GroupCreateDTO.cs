using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Group
{
    public record GroupCreateDTO (
        [Required]
        [StringLength(50, ErrorMessage ="O nome do grupo deve ter até 50 caracteres")]
        string name)
    { }
}

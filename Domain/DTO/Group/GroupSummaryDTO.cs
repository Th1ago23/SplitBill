using Domain.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Group
{
    public record GroupSummaryDTO (string name, List<UserSummaryDTO> users)
    { }
}

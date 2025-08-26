using Domain.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Expense
{
    public record DebtDTO (UserSummaryDTO fromUser, UserSummaryDTO toUser, decimal amount)
    {}
}

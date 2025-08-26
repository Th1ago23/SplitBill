using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Expense
{
    public record GroupBalanceDTO( List<DebtDTO> debts, List<MemberBalanceDTO> memberBalances )
    {}
}

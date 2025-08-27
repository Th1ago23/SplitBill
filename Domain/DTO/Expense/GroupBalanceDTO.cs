namespace Domain.DTO.Expense
{
    public record GroupBalanceDTO(List<DebtDTO> debts, List<MemberBalanceDTO> memberBalances)
    { }
}

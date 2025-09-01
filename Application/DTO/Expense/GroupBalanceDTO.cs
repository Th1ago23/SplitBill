namespace Application.DTO.Expense
{
    public record GroupBalanceDTO(List<DebtDTO> debts, List<MemberBalanceDTO> memberBalances)
    { }
}

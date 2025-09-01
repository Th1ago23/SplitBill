namespace Application.DTO.Expense
{
    public record ExpenseSummaryDTO(int id, string description, double value, string payerName)
    {
    }
}

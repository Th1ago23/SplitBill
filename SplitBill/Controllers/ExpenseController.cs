using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController:ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [Authorize]
        [HttpGet("{groupId}/GetExpensesFromGroup")]
        public IActionResult GetExpensesFromGroup(int groupId)
        {
            return Ok(_expenseService.GetGroupBalanceAsync(groupId));
        }
    }
}

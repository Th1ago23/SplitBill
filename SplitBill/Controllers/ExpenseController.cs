using Domain.DTO.Expense;
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

        [Authorize]
        [HttpPost("Group/{groupId}/CreateExpense")]
        public IActionResult CreateExpense(int groupId, ExpenseCreateDTO dto)
        {
            if (dto == null) return BadRequest();

            return CreatedAtAction("Create",
                new
                {
                    group = groupId,
                    data = dto
                }
                );
        }
    }
}

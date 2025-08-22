using Domain.DTO.Group;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController:ControllerBase
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [HttpPost("GroupCreate")]
        public IActionResult CreateGroup (GroupCreateDTO request)
        {
            _service.CreateGroup(request);

            if (request == null) BadRequest("Não foi possível criar um grupo.");

            return CreatedAtAction("Create Group", new
            {
                group = request,
                message = "Grupo criado com sucesso!"
            });
        
        }
    }
}

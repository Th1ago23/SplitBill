using Domain.DTO.Group;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpPost()]
        public IActionResult AddMember (int gpId, string userEmail)
        {         
            _service.AddMember(gpId, userEmail);
            var gpName = _service.GetGroupName(gpId);


            return CreatedAtAction("Add member",
                new
                {
                    groupName = gpName,
                    usEmail = userEmail,
                    message = "Usuário adicionado ao grupo com sucesso!"
                });
                      
        }
    }
}

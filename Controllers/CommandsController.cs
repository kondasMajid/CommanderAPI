
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Commander.Models;
using Commander.Data;
using AutoMapper;
using Commander.Dtos;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase 
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {  
            _repository = repository;
            _mapper = mapper;
        }
        
        // Get api/commands 
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            var commandsItems = _repository.GetAppCommands();
            return Ok(commandsItems);
        }

        // Get api/commands/{id }
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<CommandReadDto>> GetCommandById(int id)
        {
            var commandsItem = _repository.GetCommandById(id);

            if (commandsItem!=null)
            {
                    //  return Ok(commandsItems);
                    return Ok(_mapper.Map<CommandReadDto>(commandsItem));
            }
            return NotFound();
       
        }
    }
}
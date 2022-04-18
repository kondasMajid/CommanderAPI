
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
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsItems));
        }

        // Get api/commands/{id }
        [HttpGet("{id}", Name="GetCommandById")]
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

         // Get api/commands
         [HttpPost]
         public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
         {
             var commandModel = _mapper.Map<Command>(commandCreateDto);
             _repository.CreateCommand(commandModel);

            // Save to db 
             _repository.SaveChanges();

                var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
                return CreatedAtRoute(nameof(GetCommandById), new {id = commandReadDto.Id}, commandReadDto);
         }
    }
}
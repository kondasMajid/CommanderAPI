
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Commander.Models;
using Commander.Data;
using AutoMapper;
using Commander.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandsItems = _repository.GetAppCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsItems));
        }

        // Get api/commands/{id }
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandById(int id)
        {
            var commandsItem = _repository.GetCommandById(id);

            if (commandsItem != null)
            {
                //  return Ok(commandsItems);
                return Ok(_mapper.Map<CommandReadDto>(commandsItem));
            }
            return NotFound();

        }

        // Get api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);

            // Save to db 
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new { id = commandReadDto.Id }, commandReadDto);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]

        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commamdModelFromRepo = _repository.GetCommandById(id);
            if (commamdModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commamdModelFromRepo);

            _repository.UpdateCommand(commamdModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            // checking to see if we have result to update from  our repository
            var commamdModelFromRepo = _repository.GetCommandById(id);
            if (commamdModelFromRepo == null)
            {
                return NotFound();
            }
            // Generate a new CommandUpdateDto using command from commamdModelFromRepo
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commamdModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            // Apply validation check 
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // UpdateCommand MOdel Data repository 
            _mapper.Map(commandToPatch, commamdModelFromRepo);
            // UpdateCommand command 
            _repository.UpdateCommand(commamdModelFromRepo);
            // ServiceFilterAttribute changes in our db 
            _repository.SaveChanges();
            return NotFound();
            
        }
    }
}
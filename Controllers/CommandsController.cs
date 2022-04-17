
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Commander.Models;
using Commander.Data;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase 
    {
        private readonly ICommanderRepo _repository;
        public CommandsController(ICommanderRepo repository)
        {
            _repository = repository;
        }
        // private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        // Get api/commands 
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            var commandsItems = _repository.GetAppCommands();
            return Ok(commandsItems);
        }

        // Get api/commands/id 
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<Command>> GetCommandById(int id)
        {
            var commandsItems = _repository.GetCommandById(id);
            return Ok(commandsItems);
        }
    }
}
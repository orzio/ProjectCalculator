using Microsoft.AspNetCore.Mvc;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BendingController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public BendingController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            BendingCommand bendingCommand = new BendingCommand()
            {
                BeamType = 1,
                ShapeType = 1
            };
            _commandDispatcher.DispatchAsync(bendingCommand);
            return null;
        }

        [HttpPost]
        public Task<IActionResult> Post([FromBody] BendingCommand command)
        {
            _commandDispatcher.DispatchAsync(command);
            return null;
        }
    }
}

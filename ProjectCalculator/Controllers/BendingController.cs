using Microsoft.AspNetCore.Mvc;
using ProjectCalculator.Core.Domain;
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
                Beam = new Beam() {
                        L1 = 2,
                        L2 = 4,
                        L3 = 1,
                        Q1=9,
                        Q2 = 5,
                        P = 3},
                ShapeType = 3,
                Shape = new Shape()
                {
                    B1 = 3,
                    B2 = 5,
                    H1 = 3,
                    H2 = 3
                }
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
